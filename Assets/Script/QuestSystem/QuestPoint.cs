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

    private bool playerIsNear = false;
    private string questId;
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
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameEventsManager.instance.inputEvents.onInteract -= Interact;

    }

    //To-do Add interact with this quest point
    private void Interact()
    {
        if(!playerIsNear)
        {
            return;
        }
        if(currentQuestState.Equals(QuestState.CAN_START) && startPoint && startInteract) 
        {
            GameEventsManager.instance.questEvents.StartQuest(questId);
        }
        else if(currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint && finishInteract)
        {
            GameEventsManager.instance.questEvents.FinishQuest(questId);
        }
    }


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.CompareTag("Player")) 
        { 
            playerIsNear = true;
            if (currentQuestState.Equals(QuestState.CAN_START) && startPoint && !startInteract)
            {
                GameEventsManager.instance.questEvents.StartQuest(questId);
            }
            else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint && !finishInteract)
            {
                GameEventsManager.instance.questEvents.FinishQuest(questId);
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
            
        }
    }
}
