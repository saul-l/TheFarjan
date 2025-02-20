using UnityEngine;
using TMPro;

public class ShipResourceManagerUI : MonoBehaviour
{
    // Used by ShipResourceManager. Hard dependency, but it's cleaner to have this UI stuff in separate class.
    [SerializeField] TMP_Text fuelText;
    [SerializeField] TMP_Text foodText;
    [SerializeField] TMP_Text suppliesText;
    [SerializeField] TMP_Text joyText;
    [SerializeField] TMP_Text orderText;
    [SerializeField] TMP_Text timeText;
    private const string fuelString = "Fuel";
    private const string foodString = "Food";
    private const string suppliesString = "Supplies";
    private const string joyString = "Joy";
    private const string orderString = "Order";
    private const string timeString = "Time";
    public void UpdateUI(float fuel, float hourlyFuel, float food, float hourlyFood, float supplies, float hourlySupplies, float joy, float order)
    {
        
        fuelText.text = fuelString + " " + UIUtils.ResourceString(fuel) + System.Environment.NewLine + UIUtils.SingleDigit(hourlyFuel) + " / hour";
        foodText.text = foodString + " " + UIUtils.ResourceString(food) + System.Environment.NewLine + UIUtils.SingleDigit(hourlyFood) + " / hour";
        suppliesText.text = suppliesString + " " + UIUtils.ResourceString(supplies) + System.Environment.NewLine + UIUtils.SingleDigit(hourlySupplies) + " / hour";
        joyText.text = joyString + " " + UIUtils.ResourceString(joy);
        orderText.text = orderString + " " + UIUtils.ResourceString(order);
        timeText.text = Globals.dayString;
    }
}
