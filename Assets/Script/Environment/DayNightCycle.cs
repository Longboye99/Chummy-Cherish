using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class DayNightCycle : MonoBehaviour
{
    public float second;
    public float dayLimit = 1200;
    public float tick;
    public GameObject postProcessing;
    public Volume volume;
    public TextMeshProUGUI timeDisplay;

    public bool activateLights;
    public GameObject[] lights;

    private void OnEnable()
    {

    }
    private void Start()
    {
        volume = postProcessing.GetComponent<Volume>();
    }

    private void FixedUpdate()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        second += Time.fixedDeltaTime * tick;
        if(second >= dayLimit)
        {
            GameEventsManager.instance.playerEvents.Fainted();
            second = 0;
        }
        ControlPPV();
        DisplayTime();
    }

    private void ControlPPV()
    {
        if (second >= 780 && second < 840)
        {
            volume.weight = (second - 780) / 60;
            if (activateLights == false)
            {
                if (second >= 820)
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(true);
                    }
                    activateLights = true;
                }
            }
        }
        else if (second >= 840)
        {
            volume.weight = 1;
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(true);
            }
            activateLights = true;
        }
        else
        {
            volume.weight = 0;
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(false);
            }
            activateLights = false;

        }
    }

    private void DisplayTime()
    {
        timeDisplay.text = second.ToString();
    }
}
