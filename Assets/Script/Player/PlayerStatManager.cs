using UnityEngine;
using UnityEngine.UI;

public class PlayerStatManager : MonoBehaviour
{
    public float maxStamina;
    private float currentStamina;
    [SerializeField] float staminaDrain;
    [SerializeField] float smellStaminaUse;
    [SerializeField] Slider staminaSlider;
    [SerializeField] GameObject respawnPoint;
    private GameObject player;
    private Animator animator;
    private bool isSleeping = false;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onSmelling += Smelling;
        GameEventsManager.instance.inputEvents.onInteract += Interact;
        GameEventsManager.instance.inputEvents.onMovePressed += MovePressed;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onSmelling -= Smelling;
        GameEventsManager.instance.inputEvents.onInteract -= Interact;
        GameEventsManager.instance.inputEvents.onMovePressed -= MovePressed;

    }

    private void Start()
    {
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        currentStamina = maxStamina;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
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
            player.GetComponent<Rigidbody2D>().transform.position = respawnPoint.transform.position;
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
}
