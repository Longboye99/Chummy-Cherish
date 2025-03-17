using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;

    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;
    [SerializeField] private bool startInteract = true;
    [SerializeField] private bool finishInteract = true;
    [SerializeField] private bool doQuestOnStart = false;

    [Header("Actor Info")]
    public float actorMoveSpeed;
    private List<DialogueLine> currentCutscene;

    [Header("Starting Cutscene")]
    [SerializeField]
    private List<DialogueLine> startingDialogueLine = new List<DialogueLine>();

    [Header("Finish Cutsceene")]
    [SerializeField]
    private List<DialogueLine> finishingDialogueLine = new List<DialogueLine>();

    private bool playerIsNear = false;
    private bool cutsceneIsPlaying = false;
    private bool currentDialogueOnGoing = false;
    private bool isStartingDialogue = false;
    private string questId;
    private int dialogueIndex = 0;
    private GameObject currentDialogueBox;
    private QuestState currentQuestState;
    private QuestIcon questIcon;

    private void Awake()
    {
        questId = questInfoForPoint.id;
        questIcon = GetComponentInChildren<QuestIcon>();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        GameEventsManager.instance.inputEvents.onInteract += Interact;

        if(doQuestOnStart)
        {
            if (startingDialogueLine.Count > 0)
            {
                StartCutscene(startingDialogueLine);
                isStartingDialogue = true;
            }
            else
            {
                GameEventsManager.instance.questEvents.StartQuest(questId);
            }
        }
    }


    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameEventsManager.instance.inputEvents.onInteract -= Interact;

    }

    //To-do Add interact with this quest point
    private void Interact(InputEventContext inputEventContext)
    {
        if (!inputEventContext.Equals(InputEventContext.DIALOGUE))
        {
            if (!playerIsNear)
            {
                return;
            }
            if (currentQuestState.Equals(QuestState.CAN_START) && startPoint && startInteract)
            {
                if (startingDialogueLine.Count > 0)
                {
                    StartCutscene(startingDialogueLine);
                    isStartingDialogue = true;
                }
                else
                {
                    GameEventsManager.instance.questEvents.StartQuest(questId);
                }
            }
            else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint && finishInteract)
            {
                if (finishingDialogueLine.Count > 0)
                {
                    StartCutscene(finishingDialogueLine);
                }
                else
                {
                    GameEventsManager.instance.questEvents.FinishQuest(questId);
                }
            }
        }
        
        if (cutsceneIsPlaying && inputEventContext.Equals(InputEventContext.DIALOGUE) && !currentDialogueOnGoing)
        {
            AdvanceOrFinishCutscene(currentCutscene);
            Debug.Log("Pressed Advance Cutscene");
        }
    }


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.CompareTag("Player")) 
        { 
            playerIsNear = true;
            if (currentQuestState.Equals(QuestState.CAN_START) && startPoint && !startInteract && !doQuestOnStart)
            {
                if (startingDialogueLine.Count > 0)
                {
                    StartCutscene(startingDialogueLine);
                    isStartingDialogue = true;
                }
                else
                {
                    GameEventsManager.instance.questEvents.StartQuest(questId);
                }
            }
            else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint && !finishInteract && !doQuestOnStart)
            {
                if (finishingDialogueLine.Count > 0)
                {
                    StartCutscene(finishingDialogueLine);
                }
                else
                {
                    GameEventsManager.instance.questEvents.FinishQuest(questId);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            Debug.Log("Quest " + questId + " update to state: " + currentQuestState);
            if(questIcon != null)
            {
                questIcon.SetState(currentQuestState, startPoint, finishPoint);
            }
            if(quest.state == QuestState.FINISHED)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void StartCutscene(List<DialogueLine> dialogueLines)
    {
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        GameEventsManager.instance.inputEvents.ChangeInputEventContext(InputEventContext.DIALOGUE);
        if(questIcon != null)
        {
            questIcon.gameObject.SetActive(false);
        }
        cutsceneIsPlaying = true;
        dialogueIndex = 0;
        currentCutscene = dialogueLines;
        //GameEventsManager.instance.cutsceneEvents.StartCutscene();
        Debug.Log("Starting Cutscene");
        StartCoroutine(DisplayCutscene(currentCutscene, dialogueIndex));
    }

    private void AdvanceOrFinishCutscene(List<DialogueLine> dialogueLines)
    {
        if(currentDialogueBox != null)
        {
            Destroy(currentDialogueBox.gameObject);
        }
        
        dialogueIndex++;
        if (dialogueIndex >= dialogueLines.Count) 
        {
            FinishCutscene();
        }
        else
        {
            Debug.Log("Advance Cutscene");
            StartCoroutine(DisplayCutscene(dialogueLines, dialogueIndex));
        }
    }

    private void FinishCutscene()
    {
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        GameEventsManager.instance.inputEvents.ChangeInputEventContext(InputEventContext.DEFAULT);
        if (questIcon != null)
        {
            questIcon.gameObject.SetActive(true);
        }
        cutsceneIsPlaying = false;
        Debug.Log("Cutscene Finished");
        if(isStartingDialogue)
        {
            GameEventsManager.instance.questEvents.StartQuest(questId);
        }
        else if(!isStartingDialogue)
        {
            GameEventsManager.instance.questEvents.FinishQuest(questId);
        }
        //GameEventsManager.instance.cutsceneEvents.FinishCutscene();
    }

    private IEnumerator DisplayCutscene(List<DialogueLine> dialogueLines, int i)
    {
        currentDialogueOnGoing = true;
        if (dialogueLines[i].isMovementEvent)
        {
            yield return StartCoroutine(MoveActor(dialogueLines[i].actor, dialogueLines[i].endPosition));
            Debug.Log("Moving: " + dialogueLines[i].actor);
            currentDialogueOnGoing = false;
            AdvanceOrFinishCutscene(dialogueLines);

        }
        if (dialogueLines[i].isDialogueEvent)
        {
            currentDialogueBox = Instantiate(dialogueLines[i].dialogueBoxPrefab, dialogueLines[i].dialoguePosition.transform.position, Quaternion.identity);
            Debug.Log("Displaying dialogue at index: " + i);
            currentDialogueOnGoing = false;

        }
        if (dialogueLines[i].isTeleportEvent)
        {
            TeleportActor(dialogueLines[i].actor, dialogueLines[i].teleportPos);
            Debug.Log("Teleport: " + dialogueLines[i].actor);
            AdvanceOrFinishCutscene(dialogueLines);
            currentDialogueOnGoing = false;
        }
        currentDialogueOnGoing = false;

    }

    private IEnumerator MoveActor(GameObject actor, Vector2 endpos)
    {      
        yield return StartCoroutine(actor.GetComponent<NpcMovementController>().MoveNpc(endpos));
    }

    private void TeleportActor(GameObject actor, Vector2 endpos)
    {
        actor.GetComponent<NpcMovementController>().TeleportNpc(endpos);
    }
}
