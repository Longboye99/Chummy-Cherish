using UnityEngine;
using System;

public class CutsceneManager : MonoBehaviour
{
    public bool dialogueIsPlaying = false;
    private void OnEnable()
    {
        GameEventsManager.instance.cutsceneEvents.onStartCutscene += StartCutscene;
        GameEventsManager.instance.cutsceneEvents.onFinishCutscene += FinishCutscene;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.cutsceneEvents.onStartCutscene -= StartCutscene;
        GameEventsManager.instance.cutsceneEvents.onFinishCutscene -= FinishCutscene;
 

    }

    private void StartCutscene()
    {
        
        dialogueIsPlaying = true;
    }   
    
    private void AdvanceOrFinishCutscene()
    {

    }

    private void FinishCutscene()
    {
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        dialogueIsPlaying = false;
    }
}
