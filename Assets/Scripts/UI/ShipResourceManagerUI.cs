using UnityEngine;
using TMPro;

public class ShipResourceManagerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TMP_Text fuelText;
    [SerializeField] TMP_Text foodText;
    [SerializeField] TMP_Text suppliesText;
    [SerializeField] TMP_Text joyText;
    [SerializeField] TMP_Text orderText;
    private const string fuelString = "Fuel";
    private const string foodString = "Food";
    private const string suppliesString = "Supplies";
    private const string joyString = "Joy";
    private const string orderString = "Order";

    public void UpdateUI(float fuel, float food, float supplies, float joy, float order)
    {
        
        fuelText.text = fuelString + " " + UIUtils.ResourceString(fuel);
        foodText.text = foodString + " " + UIUtils.ResourceString(food);
        suppliesText.text = suppliesString + " " + UIUtils.ResourceString(supplies);
        joyText.text = joyString + " " + UIUtils.ResourceString(joy);
        orderText.text = orderString + " " + UIUtils.ResourceString(order);
    }
}
