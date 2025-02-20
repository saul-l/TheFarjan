// Keeps track of ship resources
// Resources are consumed and added once per second according


using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipResourceManager : MonoBehaviour
{
    [SerializeField] float updateRate = 0.1f;
    float nextTime = 0f;
    // Basic resources
    private Dictionary<ShipResource, int> shipResources = new Dictionary<ShipResource, int>();
    [SerializeField] List<ShipResource> shipResourceList = new List<ShipResource>();
    [SerializeField] List<int> shipResourceAmountList = new List<int>();
    [SerializeField] GameObject shipResourceManagerUIPrefab;
    private ShipResourceManagerUI shipResourceManagerUI;

    // For calculating hourly consumption
    private SoulsManager soulsManager;
    private float previousFuelAmount;
    private float previousSuppliesAmount;

    // Currently used only for UI, but there is option to micro-optimize here with constant resource consumption rate
    private float fuelAmount;
    private float foodAmount;
    private float suppliesAmount;
    private float joyAmount;
    private float orderAmount;

    private void Start()
    {
        if(shipResourceManagerUIPrefab != null)
        { 
            GameObject tmpGO = GameObject.Instantiate(shipResourceManagerUIPrefab);
            shipResourceManagerUI = tmpGO.GetComponent<ShipResourceManagerUI>();
        }
        else
        {
            Debug.Log("shipResourceManagerUIPrefab required!");
            gameObject.SetActive(false);
        }

        soulsManager = GetComponent<SoulsManager>();
    }
    private void Update()
    {
        // Somewhat inefficient, but hey we are only doing this once every updateRate
        if(Globals.gameTime >= nextTime)
        {
            fuelAmount = 0f;
            foodAmount = 0f;
            suppliesAmount = 0f;
            joyAmount = 0f;
            orderAmount = 0f;

            shipResourceList.Clear();
            shipResourceAmountList.Clear();
            nextTime += updateRate;
            foreach(ShipResource shipResource in shipResources.Keys)
            {
                shipResourceList.Add( shipResource );
                shipResourceAmountList.Add(shipResources[shipResource]);

                switch(shipResource.resourceType)
                {
                    case (ResourceType.Fuel):
                        fuelAmount += shipResources[shipResource];
                        break;
                    case (ResourceType.Food):
                        foodAmount += shipResources[shipResource];
                        break;
                    case (ResourceType.Supplies):
                        suppliesAmount += shipResources[shipResource];
                        break;
                }
            }

            float hourlyFuelUse = Globals.dayInSeconds * (previousFuelAmount - fuelAmount)/updateRate;
            float hourlySuppliesUse = Globals.dayInSeconds * (previousSuppliesAmount - fuelAmount) / updateRate;
            float hourlyFoodUse = soulsManager.GetDailyFoodChange() / 24f;
            shipResourceManagerUI.UpdateUI(fuelAmount,hourlyFuelUse, foodAmount, hourlyFoodUse, suppliesAmount, hourlySuppliesUse, joyAmount, orderAmount);

            previousFuelAmount = fuelAmount;
            previousSuppliesAmount = suppliesAmount;
        
        }
    }
    public void addResource(ShipResource shipResource, int amount)
    {
        if (shipResources.ContainsKey(shipResource))
        {
            Debug.Log("added resources");
            shipResources[shipResource] += amount;
        }
        else
        {
            Debug.Log("added new resources");
            shipResources.Add(shipResource, amount);
        }
    }

    public bool checkResourceAvailability (ResourceType usedResourceType, int amount)
    {
        int amountOfAvailableResources = 0;

        List<ShipResource> usableResources = new List<ShipResource>();
        foreach (ShipResource shipResource in shipResources.Keys)
        {
            if (shipResource.resourceType == usedResourceType)
                usableResources.Add(shipResource);
        }

        foreach (ShipResource usedResource in usableResources)
        {
            amountOfAvailableResources+=shipResources[usedResource];
        }

        if (amountOfAvailableResources < amount)
            return false;
        else
            return true;
    }

    public bool useResource(ResourceType usedResourceType, int amount)
    {
        // uses equal amount of two resources, if there is enough

        int amountLeft = amount;
        int? prevAmountLeft = null;

        List<ShipResource> usableResources = new List<ShipResource>();
        foreach (ShipResource shipResource in shipResources.Keys)
        {
            if (shipResource.resourceType == usedResourceType)
                usableResources.Add(shipResource);
        }
        
        while (amountLeft > 0)
        {
            if (usableResources.Count < 1)
                return false;

            int dividedAmount = amountLeft / usableResources.Count;

            // if amountLeft has not changed since last cycle, it means we are out of resources
            if(prevAmountLeft != null && prevAmountLeft != amountLeft)
            {
                foreach (ShipResource usedResource in usableResources)
                {
                    shipResources.Remove(usedResource);
                }
                return false;
            }

            prevAmountLeft = amountLeft;

            foreach (ShipResource usedResource in usableResources)
            {
                if(shipResources[usedResource] <= dividedAmount)
                {
                    amountLeft -= shipResources[usedResource];
                    shipResources.Remove(usedResource);
                }
                else
                {                
                    amountLeft -= dividedAmount;
                    shipResources[usedResource] -= dividedAmount;
                }
            }
        }

        return true;
    }

    // some quests etc might use non-generic resources
    public bool useResource(ShipResource usedResource, int amount)
    {
        if (shipResources.ContainsKey(usedResource))
        {
            shipResources[usedResource] -= amount;
            if (shipResources[usedResource] < 0)
            {
                shipResources.Remove(usedResource);
                return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
