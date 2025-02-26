using System;
using UnityEditor;
using UnityEngine;

public class LevelEvents
{
    public event Action<Transform> onLevelLoaded;
    public void LoadLevel(Transform transform)
    {
        if (onLevelLoaded != null)
        {
            onLevelLoaded(transform);
        }
    }

    public event Action<SceneAsset,string> onLevelExit;
    public void ExitLevel(SceneAsset scene, string spawnName)
    {
        if (onLevelExit != null)
        {
            onLevelExit(scene, spawnName);
        }
    }
}
