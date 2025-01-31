using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [SerializeField] private GameObject canStartIcon;
    [SerializeField] private GameObject requirementNotMetToFinishIcon;
    [SerializeField] private GameObject canFinishIcon;

    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        canStartIcon.SetActive(false);
        requirementNotMetToFinishIcon.SetActive(false);
        canFinishIcon.SetActive(false);

        switch(newState)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                break;
            case QuestState.CAN_START: 
                if(startPoint) { canStartIcon.SetActive(true); }
                break;
            case QuestState.IN_PROGRESS:
                if(finishPoint) { requirementNotMetToFinishIcon.SetActive(true);}
                break;
            case QuestState.CAN_FINISH:
                if(finishPoint) { canFinishIcon?.SetActive(true); }
                break;
            case QuestState.FINISHED:
                break;
            default:
                Debug.LogWarning("Quest State not recognized by quest icon: " +  newState);
                break;
        }
    }
}
