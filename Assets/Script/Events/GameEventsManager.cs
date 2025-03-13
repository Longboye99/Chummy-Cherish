using UnityEngine;

public class GameEventsManager:MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public InputEvents inputEvents;
    public PlayerEvents playerEvents;
    public ItemEvents itemEvents;
    public QuestEvents questEvents;
    public CutsceneEvents cutsceneEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        // initialize all events
        inputEvents = new InputEvents();
        playerEvents = new PlayerEvents();
        itemEvents = new ItemEvents();
        questEvents = new QuestEvents();
        cutsceneEvents = new CutsceneEvents();
    }
}
