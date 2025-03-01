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

    public event Action<GameObject, GameObject> onPlayerTeleport;
    public void TeleportPlayer(GameObject currentArea, GameObject newArea)
    {
        if (onPlayerTeleport != null)
        {
            onPlayerTeleport(currentArea, newArea);
        }
    }

    public event Action<GameObject> onSetTargetSmell;
    public void SetTargetSmell(GameObject targetSmell)
    {
        if (onSetTargetSmell != null)
        {
            onSetTargetSmell(targetSmell);
        }
    }

    public event Action onFainted;
    public void Fainted()
    {
        if (onFainted != null)
        {
            onFainted();
        }
    }
}
