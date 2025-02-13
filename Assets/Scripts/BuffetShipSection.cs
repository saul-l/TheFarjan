using UnityEngine;

public class BuffetShipSection : ShipSection
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int[] foodConsumptionArray = null;
    private int CFAIndex = 0;
    void Awake()
    {
        Globals.dayChange += NewDay;
    }
    override public ShipSectionState EnabledState()
    {

        bool resourcesAvailable = true;
        if (Globals.gameTime >= nextTickTime)
        {
            nextTickTime += consumptionRate;
            resourcesAvailable = shipResourceManager.useResource(consumedResourceType, foodConsumptionArray[CFAIndex]);
            CFAIndex++;
            if (CFAIndex > foodConsumptionArray.Length)
                CFAIndex = 0;            
        }
        if (resourcesAvailable)
            return ShipSectionState.enabled;
        else
            return ShipSectionState.outOfResources;

    }

    void NewDay()
    {
        consumptionRate = Globals.dayInSeconds / soulsManager.GetTotalSouls();
        foodConsumptionArray = soulsManager.GetFoodConsumptionArray();
    }
}
