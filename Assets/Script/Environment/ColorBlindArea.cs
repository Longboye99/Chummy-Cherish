using NUnit.Framework.Internal.Execution;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColorBlindArea : MonoBehaviour
{
    [SerializeField] ItemColor areaColorBlindColor;
    private SpriteRenderer enteringObjectSprite;
    private ItemWorld worldItem;
    private ItemDefinitionSO worldItemInfo;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Item"))
        {
            enteringObjectSprite = otherCollider.gameObject.GetComponent<SpriteRenderer>();
            worldItem = otherCollider.GetComponent<ItemWorld>();
            worldItemInfo = worldItem.Definition;

            //Debug.Log(otherCollider.name + " :has entered grass area");

            if (worldItemInfo.color == areaColorBlindColor)
            {
                enteringObjectSprite.enabled = false;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Item"))
        {
            enteringObjectSprite = otherCollider.gameObject.GetComponent<SpriteRenderer>();

            //Debug.Log(otherCollider.name + " :has exited grass area");

            if (worldItemInfo.color == areaColorBlindColor)
            {
                enteringObjectSprite.enabled = true;
            }
        }
    }
}
