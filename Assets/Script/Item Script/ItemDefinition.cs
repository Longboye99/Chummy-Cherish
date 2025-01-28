using UnityEngine;

[CreateAssetMenu(fileName = "ItemDefinition", menuName = "Scriptable Objects/ItemDefinition")]
public class ItemDefinition : ScriptableObject
{
    public string Label = "player-facing name of this object";// ie. no "stick_0001" but "Stick Of Mighty Powers"
    public GameObject PlayerPrefab = null;
    public GameObject WorldPrefab = null;
    // public float Damage;// etc
}
