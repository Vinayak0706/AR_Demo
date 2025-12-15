using UnityEngine;

[CreateAssetMenu(menuName = "AR/Placeable Object")]
public class PlaceableObjectData : ScriptableObject
{
    public string displayName;
    public GameObject prefab;
    public Vector3 defaultScale = Vector3.one;
}
