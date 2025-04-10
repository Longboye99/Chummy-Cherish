using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] GameObject dangerArea;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] PlayerMovementController movementController;
    [SerializeField] AudioSource barkingSound;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            dangerArea.SetActive(true);
            playerManager.staminaDrain += 10;
            movementController.moveSpeed = movementController.moveSpeed * 0.3f;
            barkingSound.enabled = true;
        }       
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            dangerArea.SetActive(false);
            playerManager.staminaDrain = 0.5f;
            movementController.moveSpeed = 20;
            barkingSound.enabled = false;

        }
    }
}
