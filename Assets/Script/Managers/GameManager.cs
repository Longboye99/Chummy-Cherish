using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance { get; private set; }
    public PlayerManager PlayerManager;
    public LevelManager LevelManager;

    [SerializeField] SpawnLocationSO defaultSpawn;
    public SpawnLocationSO spawnLocation;


    private void Awake()
    {
        if(Instance  != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        spawnLocation = Instantiate(defaultSpawn);
        PlayerManager.spawnLocation = spawnLocation;
        LevelManager.spawnLocation = spawnLocation;
    }
}
