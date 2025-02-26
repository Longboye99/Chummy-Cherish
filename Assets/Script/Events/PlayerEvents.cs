using System;
using UnityEngine;

public class PlayerEvents
{
    public event Action onDisablePlayerMovement;
    public void DisablePlayerMovement()
    {
        if (onDisablePlayerMovement != null)
        {
            onDisablePlayerMovement();
        }
    }

    public event Action onEnablePlayerMovement;
    public void EnablePlayerMovement()
    {
        if (onEnablePlayerMovement != null)
        {
            onEnablePlayerMovement();
        }
    }

    public event Action<Transform> onPlayerSpawned;
    public void PlayerSpawned(Transform defaultSpawnTransform)
    {
        if (onPlayerSpawned != null)
        {
            onPlayerSpawned(defaultSpawnTransform);
        }
    }

    public event Action onPlayerDespawned;
    public void PlayerDespawned()
    {
        if (onPlayerDespawned != null)
        {
            onPlayerDespawned();
        }
    }

}
