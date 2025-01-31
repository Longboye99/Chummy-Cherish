using UnityEngine;

public class SleepingArea : MonoBehaviour
{
    public bool playerIsNear = false;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            Debug.Log("Player is on bed");
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            Debug.Log("Player has exited bed");

            playerIsNear = false;
        }
    }
}
