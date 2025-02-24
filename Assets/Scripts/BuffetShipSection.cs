using UnityEngine;

public class BuffetShipSection : ShipSection
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int[] foodConsumptionArray = null;
    private int CFAIndex = 0;
    override public void Start()
    {

        base.Start();
        Port port = GameObject.FindGameObjectWithTag("Port").GetComponent<Port>();

        if (port != null)
            port.embarkDelegate += Embark;

    }
    override public ShipSectionState EnabledState()
    {
        bool resourcesAvailable = true;
        if (Globals.instance.gameTime >= nextTickTime && !Globals.instance.paused)
        {
            nextTickTime += consumptionRate;
            resourcesAvailable = shipResourceManager.UseResource(consumedResourceType, -foodConsumptionArray[CFAIndex]);
            CFAIndex++;
            if (CFAIndex > foodConsumptionArray.Length)
                CFAIndex = 0;            
        }
        if (resourcesAvailable)
            return ShipSectionState.enabled;
        else
            return ShipSectionState.outOfResources;
    }

    void Embark()
    {
        consumptionRate = Globals.dayInMinutes / soulsManager.GetTotalSouls();
        foodConsumptionArray = soulsManager.GetFoodConsumptionArray();
        CFAIndex = 0;
    }
}
