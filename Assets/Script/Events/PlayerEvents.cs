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

    public event Action<bool> onPlayerSleeping;
    public void PlayerSleeping(bool isSleeping)
    {
        if (onPlayerSleeping != null)
        {
            onPlayerSleeping(isSleeping);
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

    public event Action<float> onIncreaseStamina;
    public void IncreaseStamina(float amount)
    {
        if (onIncreaseStamina != null)
        {
            onIncreaseStamina(amount);
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
