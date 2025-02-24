// Keeps track of ship resources
// Resources are consumed and added once per second according


using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipResourceManager : MonoBehaviour
{

    // Basic resources
    public Dictionary<ShipResource, int> shipResources = new Dictionary<ShipResource, int>();

    public void addResource(ShipResource shipResource, int amount)
    {
        if (shipResources.ContainsKey(shipResource))
        {
            Debug.Log("added resources" + shipResource.resourceType);
            shipResources[shipResource] += amount;
        }
        else
        {
            Debug.Log("added new resources" + shipResource.resourceType);
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
            { 
                usableResources.Add(shipResource);
            }
            Debug.Log(shipResource.resourceType + " " + usedResourceType);
        }

        Debug.Log("amount left " + amountLeft);

        while (amountLeft > 0)
        {
            Debug.Log("usableResource.count " + usableResources.Count);
            if (usableResources.Count < 1)
                return false;

            int dividedAmount = amountLeft / usableResources.Count;
            Debug.Log("dividedAmount " + dividedAmount);
            // if amountLeft has not changed since last cycle, it means we are out of resources
            if (prevAmountLeft != null && prevAmountLeft != amountLeft)
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
