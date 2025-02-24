using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ShipResourceManagerUI : MonoBehaviour
{
    [SerializeField] List<ShipResource> shipResourceList = new List<ShipResource>();
    [SerializeField] List<int> shipResourceAmountList = new List<int>();

    [SerializeField] float updateRate = 0.1f;
    float nextTime = 0f;

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

    public ShipResourceManager shipResourceManager;
    public SoulsManager soulsManager;

    // For calculating hourly consumption
    private float previousFuelAmount;
    private float previousSuppliesAmount;

    // Currently used only for UI, but there is option to micro-optimize here with constant resource consumption rate
    private float fuelAmount;
    private float foodAmount;
    private float suppliesAmount;
    private float joyAmount;
    private float orderAmount;

    private void Update()
    {
        // Somewhat inefficient, but hey we are only doing this once every updateRate
        if (Time.time >= nextTime)
        {
            fuelAmount = 0f;
            foodAmount = 0f;
            suppliesAmount = 0f;
            joyAmount = 0f;
            orderAmount = 0f;

            shipResourceList.Clear();
            shipResourceAmountList.Clear();
            nextTime += updateRate;
            foreach (ShipResource shipResource in shipResourceManager.shipResources.Keys)
            {
                shipResourceList.Add(shipResource);
                shipResourceAmountList.Add(shipResourceManager.shipResources[shipResource]);

                switch (shipResource.resourceType)
                {
                    case (ResourceType.Fuel):
                        fuelAmount += shipResourceManager.shipResources[shipResource];
                        break;
                    case (ResourceType.Food):
                        foodAmount += shipResourceManager.shipResources[shipResource];
                        break;
                    case (ResourceType.Supplies):
                        suppliesAmount += shipResourceManager.shipResources[shipResource];
                        break;
                }
            }

            float hourlyFuelUse = Globals.dayInMinutes * (previousFuelAmount - fuelAmount) / updateRate;
            float hourlySuppliesUse = Globals.dayInMinutes * (previousSuppliesAmount - fuelAmount) / updateRate;
            float hourlyFoodUse = soulsManager.GetDailyFoodChange() / 24f;
            UpdateUI(fuelAmount, hourlyFuelUse, foodAmount, hourlyFoodUse, suppliesAmount, hourlySuppliesUse, joyAmount, orderAmount);

            previousFuelAmount = fuelAmount;
            previousSuppliesAmount = suppliesAmount;

        }
    }

    public void UpdateUI(float fuel, float hourlyFuel, float food, float hourlyFood, float supplies, float hourlySupplies, float joy, float order)
    {
        // get amounts
        fuelText.text = fuelString + " " + UIUtils.ResourceString(fuel) + System.Environment.NewLine + UIUtils.SingleDigit(hourlyFuel) + " / hour";
        foodText.text = foodString + " " + UIUtils.ResourceString(food) + System.Environment.NewLine + UIUtils.SingleDigit(hourlyFood) + " / hour";
        suppliesText.text = suppliesString + " " + UIUtils.ResourceString(supplies) + System.Environment.NewLine + UIUtils.SingleDigit(hourlySupplies) + " / hour";
        joyText.text = joyString + " " + UIUtils.ResourceString(joy);
        orderText.text = orderString + " " + UIUtils.ResourceString(order);
        timeText.text = Globals.instance.dayString;
    }
}
