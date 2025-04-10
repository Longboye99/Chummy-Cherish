using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private GameObject scene1;
    [SerializeField] private GameObject scene2;
    private bool playCutscene1 = false;
    private bool playDreamScene = false;
    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onFinishQuest += Cutscene1;
        GameEventsManager.instance.inputEvents.onInteract += Cutscene2;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onFinishQuest -= Cutscene1;
        GameEventsManager.instance.inputEvents.onInteract -= Cutscene2;

    }

    private void Cutscene1(string id)
    {
        if(id == "FinalQuest")
        {
            scene1.SetActive(true);
            playCutscene1 = true ;
        }
    }

    private void Cutscene2(InputEventContext inputEventContext)
    {
        if (playCutscene1)
        {
            scene1.SetActive(false);

            scene2.SetActive(true);
            playCutscene1 = false;
            playDreamScene = true ;
        }
        else if (playDreamScene)
        {
            SceneManager.LoadScene(2);
        }
    }
}
