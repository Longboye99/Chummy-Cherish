using UnityEngine;
using System;

public class ItemEvents
{
    public event Action<ItemDefinitionSO> onEquipItem;
    public void EquipItem(ItemDefinitionSO id)
    {
        if (onEquipItem != null)
        {
            onEquipItem(id);
        }
    }

    public event Action<ItemDefinitionSO> onUnequipItem;
    public void UnequipItem(ItemDefinitionSO id)
    {
        if (onUnequipItem != null)
        {
            onUnequipItem(id);
        }
    }
}
