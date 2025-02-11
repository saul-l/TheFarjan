using UnityEngine;

public enum ResourceType
{
    None,
    Food,
    Fuel,
    Supplies,
    Special
};

[CreateAssetMenu(fileName = "ShipResource", menuName = "Scriptable Objects/ShipResource")]
public class ShipResource : ScriptableObject
{
    public string resourceName = string.Empty;
    public string description = string.Empty;
    public ResourceType resourceType = ResourceType.Food;
}
