using UnityEngine;
using System;

public class CutsceneEvents : MonoBehaviour
{
    public event Action onStartCutscene;
    public void StartCutscene()
    {
        if (onStartCutscene != null)
        {
            onStartCutscene();
        }
    }

    public event Action onAdvanceOrFinishCutscene;
    public void AdvanceOrFinishCutscene()
    {
        if (onAdvanceOrFinishCutscene != null)
        {
            onAdvanceOrFinishCutscene();
        }
    }

    public event Action onFinishCutscene;
    public void FinishCutscene()
    {
        if (onFinishCutscene != null)
        {
            onFinishCutscene();
        }
    }
}
