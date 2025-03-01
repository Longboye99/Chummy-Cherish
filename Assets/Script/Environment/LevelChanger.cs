using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] public GameObject currentArea;
    [SerializeField] public GameObject newArea;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            ChangeLevel(collider.gameObject);
        }
    }

    public void ChangeLevel(GameObject player)
    {
        currentArea.SetActive(false);
        newArea.SetActive(true);
        // GameEventsManager.instance.playerEvents.TeleportPlayer(currentArea, newArea);
        player.transform.position = spawnPosition.position;
    }

}
