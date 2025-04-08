using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Config")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    public Vector2 velocity = Vector2.zero;

    private bool movementDisabled = false;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameEventsManager.instance.inputEvents.onMovePressed += MovePressed;
        GameEventsManager.instance.playerEvents.onDisablePlayerMovement += DisablePlayerMovement;
        GameEventsManager.instance.playerEvents.onEnablePlayerMovement += EnablePlayerMovement;
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.inputEvents.onMovePressed -= MovePressed;
        GameEventsManager.instance.playerEvents.onDisablePlayerMovement -= DisablePlayerMovement;
        GameEventsManager.instance.playerEvents.onEnablePlayerMovement -= EnablePlayerMovement;
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void FixedUpdate()
    {       
        rb.linearVelocity = velocity;
        
    }

    private void UpdateAnimation()
    {
        speed = Vector3.Magnitude(rb.linearVelocity);

        if (velocity.x != 0 || velocity.y != 0)
        {

            animator.SetFloat("velocity_x", velocity.x);
            animator.SetFloat("velocity_y", velocity.y);
            animator.SetFloat("speed", speed);
        }
        else
        {
            animator.SetFloat("speed", 0);
        }
    }

    private void MovePressed(Vector2 moveDir)
    {
        velocity = moveDir.normalized * moveSpeed;
        if (movementDisabled)
        {
            velocity = Vector2.zero;
        }
    }

    private void DisablePlayerMovement()
    {
        movementDisabled = true;
        // also ensure we stop any current movement
        velocity = Vector2.zero;
    }

    private void EnablePlayerMovement()
    {
        movementDisabled = false;
    }
}
