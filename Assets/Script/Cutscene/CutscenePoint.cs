using System.Collections.Generic;
using UnityEngine;

public class CutscenePoint : MonoBehaviour
{
    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    [Header("Dialogue")]
    [SerializeField]
    private List<DialogueLine>dialogueLine = new List<DialogueLine>();
    private void QuestStateChange(Quest quest)
    {
    }
}
