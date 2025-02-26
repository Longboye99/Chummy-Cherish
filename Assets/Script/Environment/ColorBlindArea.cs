using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColorBlindArea : MonoBehaviour
{
    [SerializeField] string colorblindColor;
    private SpriteRenderer enteringObjectSprite;


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        enteringObjectSprite = otherCollider.gameObject.GetComponent<SpriteRenderer>();

        //Debug.Log(otherCollider.name + " :has entered grass area");

        if (otherCollider.CompareTag(colorblindColor))
        {
            enteringObjectSprite.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        enteringObjectSprite = otherCollider.gameObject.GetComponent<SpriteRenderer>();

        //Debug.Log(otherCollider.name + " :has exited grass area");

        if (otherCollider.CompareTag(colorblindColor))
        {
            enteringObjectSprite.enabled = true;
        }
    }
}
