using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float maxStamina;
    private float currentStamina;
    public float staminaDrain;
    [SerializeField] float smellStaminaUse;
    [SerializeField] Slider staminaSlider;
    [SerializeField] GameObject respawnPoint;
    public GameObject spawnPointCam;
    private GameObject player;
    private Animator animator;
    [SerializeField] private Animator transitionAnim;
    private bool isSleeping = false;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onSmelling += Smelling;
        GameEventsManager.instance.inputEvents.onInteract += Interact;
        GameEventsManager.instance.inputEvents.onMovePressed += MovePressed;
        GameEventsManager.instance.playerEvents.onFainted += Fainted;
        GameEventsManager.instance.playerEvents.onIncreaseStamina += IncreaseStamina;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onSmelling -= Smelling;
        GameEventsManager.instance.inputEvents.onInteract -= Interact;
        GameEventsManager.instance.inputEvents.onMovePressed -= MovePressed;
        GameEventsManager.instance.playerEvents.onFainted -= Fainted;
        GameEventsManager.instance.playerEvents.onIncreaseStamina -= IncreaseStamina;

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

    private void Interact(InputEventContext inputEventContext)
    {
        if (respawnPoint.GetComponent<SleepingArea>().playerIsNear && inputEventContext.Equals(InputEventContext.DEFAULT))
        {
            isSleeping = true;
            GameEventsManager.instance.playerEvents.DisablePlayerMovement();
            GameEventsManager.instance.playerEvents.PlayerSleeping(true);
        }
        else
        {
            isSleeping = false;
            GameEventsManager.instance.playerEvents.PlayerSleeping(false);

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
            Fainted();
        }

        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
    }

    private void IncreaseStamina(float amount)
    {
        currentStamina += amount;
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
                GameEventsManager.instance.playerEvents.PlayerSleeping(false);
            }
        }     
    }
    
    private void Fainted()
    {
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        animator.SetBool("IsSleeping", isSleeping);
        transitionAnim.SetTrigger("end");
        transitionAnim.SetTrigger("start");

        if (!isSleeping)
        {
            spawnPointCam.SetActive(true);
            player.GetComponent<Rigidbody2D>().transform.position = respawnPoint.transform.position;
            currentStamina = maxStamina - 40;
        }
        else
        {
            currentStamina = maxStamina;
        }
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
    }

}
