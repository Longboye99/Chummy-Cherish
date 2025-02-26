using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public SpawnLocationSO spawnLocation;

    private void OnEnable()
    {
        GameEventsManager.instance.levelEvents.onLevelExit += ExitLevel;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.levelEvents.onLevelExit -= ExitLevel;
    }

    private void ExitLevel(SceneAsset sceneToLoad, string spawnName)
    {
        spawnLocation.playerSpawnLocation = spawnName;
        SceneManager.LoadScene(sceneToLoad.name, LoadSceneMode.Single);
    }
    
}
