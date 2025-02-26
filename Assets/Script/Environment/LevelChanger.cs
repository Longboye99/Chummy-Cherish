using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.transform.position = spawnPosition.position;
        }
    }
}
