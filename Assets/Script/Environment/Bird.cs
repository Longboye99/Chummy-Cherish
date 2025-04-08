using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Flyaway()
    {

    }
}
