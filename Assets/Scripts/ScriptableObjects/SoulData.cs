using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SoulData", menuName = "Scriptable Objects/SoulData")]


public class SoulData : ScriptableObject
{
    public int crewMemberStatus = 0; // modifier for some values
    public int dailyFoodChange = -1;
    public int dailyJoyChange = -1;
    public int dailyOrderChange = 0;
    public int dailySuppliesChange = 0;
    public string characterName;
    public string ability1Description;
    public string ability2Description;
    public string ability3Description;
    public string characterDescription;
    public Sprite characterImage;

}
