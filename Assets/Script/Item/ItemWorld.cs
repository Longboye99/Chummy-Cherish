using UnityEngine;

// SOLE RESPONSIBILITY:
// Provide a reference to relevant ItemDefinition. Nothing else.

public class ItemWorld : MonoBehaviour
{
    public ItemDefinitionSO Definition;

    private void OnEnable()
    {
        transform.position = transform.position + Vector3.zero;
    }
}