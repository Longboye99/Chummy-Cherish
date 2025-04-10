using UnityEngine;

public class AmbienceSound : MonoBehaviour
{
    [SerializeField] AudioSource ambienceSound; 

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            ambienceSound.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            ambienceSound.enabled = false;
        }
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        
    }*/
}
