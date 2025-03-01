using System.Collections.Generic;
using UnityEngine;

// SOLE RESPONSIBILITY:
// Enables item picking up and dropping ability. Nothing else.
public class ItemController : MonoBehaviour
{

    [Header("Required fields")]
    public Transform Socket = null;// picked up items goes here

    [Header("Read-Only fields")]
    public ItemDefinitionSO EquipedInfo = null;
    public string ItemId = null;
    public GameObject Equipped = null;
    public List<ItemWorld> Contacts = new List<ItemWorld>();
    private Rigidbody2D rb;


    private void Start()
    {
        GameEventsManager.instance.inputEvents.onItemEquipped += Equip;
        GameEventsManager.instance.inputEvents.onItemUnequipped += Unequip;
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.inputEvents.onItemEquipped -= Equip;
        GameEventsManager.instance.inputEvents.onItemUnequipped -= Unequip;
    }

    void Equip()
    {
        if (Equipped != null &&  Contacts.Count == 0) Unequip();
        else if (Contacts.Count != 0)
        {
            ItemWorld worldItem = Contacts[0];
            ItemDefinitionSO worldItemInfo = worldItem.Definition;
            Destroy(worldItem.gameObject);

            if (Equipped != null) Unequip();

            Equipped = Instantiate(worldItemInfo.PlayerPrefab, Socket);
            Equipped.transform.localPosition = Vector3.zero;
            Equipped.transform.localRotation = Quaternion.identity;
            EquipedInfo = worldItemInfo;
            ItemId = worldItemInfo.id;
            GameEventsManager.instance.itemEvents.EquipItem(EquipedInfo);
            Debug.Log($"ItemController : {EquipedInfo.Label} equipped");
        }
        else Debug.Log("ItemController : no items to equip");
    }

    void Unequip()
    {
        if (Equipped != null)
        {
            Destroy(Equipped);
            GameObject droppedAsWorldItem = Instantiate(EquipedInfo.WorldPrefab, transform.position, Quaternion.identity);
            Debug.Log($"ItemController : {EquipedInfo.Label} dropped");
            GameEventsManager.instance.itemEvents.UnequipItem(EquipedInfo);
            Equipped = null;
            EquipedInfo = null;
            ItemId = null;
            

        }
        else Debug.Log("ItemController : nothing to unequip");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld item = collision.GetComponent<ItemWorld>();
        if (item != null) Contacts.Add(item);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        ItemWorld item = collision.GetComponent<ItemWorld>();
        if (item != null) Contacts.Remove(item);
    }

}