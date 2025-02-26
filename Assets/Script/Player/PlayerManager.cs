using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float maxStamina;
    private float currentStamina;

    [SerializeField] float staminaDrain;
    [SerializeField] float smellStaminaUse;
    [SerializeField] Slider staminaSlider;
    [SerializeField] GameObject respawnPoint;
    [SerializeField] string spawnTag = "";

    public SpawnLocationSO spawnLocation;
    private GameObject ActivePlayer;
    [SerializeField] GameObject PlayerPrefab;
    private Animator animator;
    private bool isSleeping = false;
    

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onSmelling += Smelling;
        GameEventsManager.instance.inputEvents.onInteract += Interact;
        GameEventsManager.instance.inputEvents.onMovePressed += MovePressed;
        GameEventsManager.instance.levelEvents.onLevelLoaded += LoadedLevel;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onSmelling -= Smelling;
        GameEventsManager.instance.inputEvents.onInteract -= Interact;
        GameEventsManager.instance.inputEvents.onMovePressed -= MovePressed;
        GameEventsManager.instance.levelEvents.onLevelLoaded -= LoadedLevel;


    }

    private void Start()
    {
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        currentStamina = maxStamina;
        ActivePlayer = GameObject.FindGameObjectWithTag("Player");
        animator = ActivePlayer.GetComponent<Animator>();
    }

    private void Update()
    {        
        UpdateStamina();
        UpdateAnimation();


    }

    private void Smelling()
    {
        currentStamina = currentStamina - smellStaminaUse;
    }

    private void Interact()
    {
        if (respawnPoint.GetComponent<SleepingArea>().playerIsNear)
        {
            isSleeping = true;
            GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        }
        else
        {
            isSleeping = false;
        }
    }

    private void UpdateStamina()
    {
        
        if (isSleeping)
        {
            currentStamina += 5f * Time.deltaTime;
        }
        else
        {
            currentStamina -= staminaDrain * Time.deltaTime;
            
        }

        staminaSlider.value = currentStamina;

        if (currentStamina <= 0)
        {
            ActivePlayer.GetComponent<Rigidbody2D>().transform.position = respawnPoint.transform.position;
            currentStamina = maxStamina - 20;
        }

        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
    }

    private void UpdateAnimation()
    {
        animator.SetBool("IsSleeping", isSleeping);
    }

    private void MovePressed(Vector2 movDir)
    {
        if(movDir != null && isSleeping)
        {
            if (respawnPoint.GetComponent<SleepingArea>().playerIsNear)
            {
                isSleeping = false;
                GameEventsManager.instance.playerEvents.EnablePlayerMovement();

            }
        }
        
    }

    private void LoadedLevel(Transform defaultSpawnTransform)
    {
        if(spawnLocation.playerSpawnLocation != "")
        {
            GameObject[] spawns = GameObject.FindGameObjectsWithTag(spawnTag);
            bool foundSpawn = false;

            foreach (GameObject spawn in spawns)
            {
                if (spawn.name == spawnTag)
                {
                    if (spawn.name == spawnLocation.playerSpawnLocation)
                    {
                        ActivePlayer = Instantiate(PlayerPrefab, spawn.transform.position, Quaternion.identity);
                        break;
                    }
                }
            }
            if (!foundSpawn)
            {
                throw new MissingReferenceException("Couldn't find the player spawn location with the name: " + spawnLocation.playerSpawnLocation);
            }
        }
        else
        {
            ActivePlayer = Instantiate(PlayerPrefab, defaultSpawnTransform.position, Quaternion.identity);
            Debug.Log("Player spawned at default location");
        }

        //Set camera to player
        
    }
}
