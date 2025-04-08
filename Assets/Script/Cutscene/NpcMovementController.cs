using System.Collections;
using UnityEngine;

public class NpcMovementController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    public Vector2 velocity = Vector2.zero;
    public Vector2 finalPosition;
    public float distance;

    private bool movementDisabled = false;
    private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        velocity = Vector2.zero;
        if (!gameObject.CompareTag("Player"))
        {
            movementDisabled = true;
        }
    }

    private void Update()
    {
        if(animator != null && !gameObject.CompareTag("Player")) UpdateAnimation();    
    }

    private void FixedUpdate()
    {
        if (movementDisabled)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else if(!gameObject.CompareTag("Player"))
        {
            rb.linearVelocity = velocity;
        }
    }

    public IEnumerator MoveNpc(Vector2 endpos)
    {
        movementDisabled = false;
        finalPosition = endpos;
        Vector2 moveDir = (finalPosition - (Vector2)rb.transform.position);
        velocity = moveDir.normalized * moveSpeed;

        while (Vector2.Distance(this.transform.position, finalPosition) > 0.3f)
        {
            yield return null;
        }
        if (gameObject.CompareTag("Player"))
        {
            movementDisabled = false;
        }
        else
        {
            movementDisabled = true;
        }
        
        transform.position = finalPosition;
    }

    public void TeleportNpc(Vector2 teleportPos)
    {
        transform.position = teleportPos;
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
}
