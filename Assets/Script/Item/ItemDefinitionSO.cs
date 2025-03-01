using UnityEngine;

[CreateAssetMenu(fileName = "ItemDefinition", menuName = "Scriptable Objects/ItemDefinition")]
public class ItemDefinitionSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }
    public string Label = "player-facing name of this object";// ie. no "stick_0001" but "Stick Of Mighty Powers"
    public GameObject PlayerPrefab = null;
    public GameObject WorldPrefab = null;

    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
    public ItemColor color;
    public bool isFood;
    public float staminaAmount;
}
