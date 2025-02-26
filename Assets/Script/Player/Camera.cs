using UnityEngine;

public class Camera : MonoBehaviour
{

    [Header("Transform to Follow")]
    [SerializeField] private Transform targetTransform;

    [Header("Configuration")]
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;
    [SerializeField] private Vector2 offset = Vector2.zero;

    private Transform originalTargetTransform;

    private void Start()
    {
        originalTargetTransform = targetTransform;
    }

    private void LateUpdate()
    {
        // if we don't have a target transform, don't update
        if (targetTransform == null)
        {
            return;
        }

        float newPosX = this.transform.position.x;
        float newPosY = this.transform.position.y;
        if (followX)
        {
            newPosX = targetTransform.position.x + offset.x;
        }
        if (followY)
        {
            newPosY = targetTransform.position.y + offset.y;
        }
        this.transform.position = new Vector3(newPosX, newPosY, this.transform.position.z);
    }
}
