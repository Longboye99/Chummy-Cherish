using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public SceneAsset sceneToLoad;
    public string spawnName = "";

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GameEventsManager.instance.levelEvents.ExitLevel(sceneToLoad, spawnName);
        }
    }

}


