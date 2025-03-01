using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public enum ShipSectionState
{
    disabled,
    outOfResources,
    enabled,
    boosted
}
public class ShipSection : MonoBehaviour
{
    [SerializeField] protected ShipSectionState shipSectionState = ShipSectionState.enabled;

    [SerializeField] protected ShipResourceManager shipResourceManager = null;
    protected SoulsManager soulsManager = null;

    [SerializeField] protected ResourceType consumedResourceType;
    [SerializeField] protected float consumptionRate = 1.0f;
    [SerializeField] protected int consumptionAmount = 10;
    [SerializeField] protected bool enableRequest = false;
    protected float nextTickTime = 0f;

    virtual public void Start()
    {
        soulsManager = GetComponentInParent<SoulsManager>();
        if (soulsManager == null)
        {
            gameObject.SetActive(false);
            Debug.Log("No SoulsManager found in parent!");
        }

        if (shipResourceManager == null )
        {
            gameObject.SetActive(false);
            Debug.Log("No ShipResourceManager found in parent!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(shipSectionState)
        {
            case ShipSectionState.disabled:
                shipSectionState = DisabledState();
                break;
        
            case ShipSectionState.outOfResources:
                shipSectionState = OutOfResourcesState();
                break;
        
            case ShipSectionState.enabled:
                shipSectionState = EnabledState();
                break;

            case ShipSectionState.boosted:
                break;
        }
    }

    ShipSectionState DisabledState()
    {
        if (enableRequest && shipResourceManager.CheckResourceAvailability(consumedResourceType, consumptionAmount))
        {
            enableRequest = false;
            nextTickTime = Globals.instance.gameTime + consumptionRate;
            return ShipSectionState.enabled;
        }

        else return ShipSectionState.disabled;
    }
    ShipSectionState OutOfResourcesState()
    {
        bool resourcesAvailable = false;
        if (Globals.instance.gameTime >= nextTickTime)
        {
            nextTickTime += consumptionRate;
            resourcesAvailable = shipResourceManager.UseResource(consumedResourceType, consumptionAmount);
        }
        if (resourcesAvailable)
            return ShipSectionState.enabled;
        else
            return ShipSectionState.outOfResources;

    }

    virtual public ShipSectionState EnabledState()
    {
        bool resourcesAvailable = true;
        if (Globals.instance.gameTime >= nextTickTime)
        {
            nextTickTime += consumptionRate;
            resourcesAvailable = shipResourceManager.UseResource(consumedResourceType, consumptionAmount);                      
        }
        if (resourcesAvailable)
            return ShipSectionState.enabled;
        else
            return ShipSectionState.outOfResources;
    }
}
