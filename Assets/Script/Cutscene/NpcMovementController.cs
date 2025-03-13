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

    private bool movementDisabled = true;
    private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        velocity = Vector2.zero;
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        if (movementDisabled)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else
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

        while (Vector2.Distance(this.transform.position, finalPosition) > 0.2f)
        {
            yield return null;
        }
        movementDisabled = true;
        transform.position = finalPosition;
        /*while (Vector2.Distance(this.transform.position, finalPosition) > 0.2f)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, endpos, step);
            yield return null;
        }
        transform.position = endpos;*/

    }

    private void UpdateAnimation()
    {
        speed = Vector3.Magnitude(rb.linearVelocity);

        if (velocity.x != 0 || velocity.y != 0)
        {

            animator.SetFloat("Velocity_x", velocity.x);
            animator.SetFloat("Velocity_y", velocity.y);
            animator.SetFloat("Speed", speed);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }
}
