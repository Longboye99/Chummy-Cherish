using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColorBlind : MonoBehaviour
{
    [SerializeField] string baseColor;
    private SpriteRenderer enteringObjectSprite;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        enteringObjectSprite = otherCollider.gameObject.GetComponent<SpriteRenderer>();
        if (otherCollider.CompareTag(baseColor))
        {
            enteringObjectSprite.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        enteringObjectSprite = otherCollider.gameObject.GetComponent<SpriteRenderer>();
        if (otherCollider.CompareTag(baseColor))
        {
            enteringObjectSprite.enabled = true;
        }
    }
}
