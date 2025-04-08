using System;
using UnityEngine;

public class InputEvents
{
    public InputEventContext inputEventContext { get; private set; } = InputEventContext.DEFAULT;

    public void ChangeInputEventContext(InputEventContext newContext)
    {
        this.inputEventContext = newContext;
    }


    public event Action<Vector2> onMovePressed;
    public void MovePressed(Vector2 moveDir)
    {
        if (onMovePressed != null)
        {
            onMovePressed(moveDir);
        }
    }

    public event Action onItemEquipped;
    public void Equip()
    {
        if (onItemEquipped != null)
        {
            onItemEquipped();
        }
    }

    public event Action onItemUnequipped;
    public void Unequip()
    {
        if (onItemUnequipped != null)
        {
            onItemUnequipped();
        }
    }

    public event Action<InputEventContext> onInteract;
    public void Interact()
    {
        if (onInteract != null)
        {
            onInteract(this.inputEventContext);
        }
    }

    public event Action onSmelling;
    public void Smelling()
    {
        if (onSmelling != null)
        {
            onSmelling();
        }
    }

    public event Action onBarking;
    public void Barking()
    {
        if (onBarking != null)
        {
            onBarking();
        }
    }
}
