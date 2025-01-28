using System.Collections.Generic;
using UnityEngine;

// SOLE RESPONSIBILITY:
// Enables item picking up and dropping ability. Nothing else.
public class ItemController : MonoBehaviour
{

    [Header("Required fields")]
    public Transform Socket = null;// picked up items goes here

    [Header("Read-Only fields")]
    public ItemDefinition EquipedInfo = null;
    public GameObject Equipped = null;
    public List<ItemWorld> Contacts = new List<ItemWorld>();
    private Rigidbody2D _rb;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Equip();
        if (Input.GetKeyDown(KeyCode.Q)) Unequip();
    }

    void Equip()
    {
        if (Contacts.Count != 0)
        {
            ItemWorld worldItem = Contacts[0];
            ItemDefinition worldItemInfo = worldItem.Definition;
            Destroy(worldItem.gameObject);
            if (Equipped != null) Unequip();
            Equipped = Instantiate(worldItemInfo.PlayerPrefab, Socket);
            Equipped.transform.localPosition = Vector3.zero;
            Equipped.transform.localRotation = Quaternion.identity;
            EquipedInfo = worldItemInfo;
            Debug.Log($"{EquipedInfo.Label} equipped");
        }
        else Debug.Log("no items to equip");
    }

    void Unequip()
    {
        if (Equipped != null)
        {
            Destroy(Equipped);
            GameObject droppedAsWorldItem = Instantiate(EquipedInfo.WorldPrefab, Socket.transform.position, Quaternion.identity);
            Debug.Log($"{EquipedInfo.Label} dropped");
            Equipped = null;
            EquipedInfo = null;
        }
        else Debug.Log("nothing to unequip");
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