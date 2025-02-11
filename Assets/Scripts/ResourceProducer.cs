using UnityEngine;

// Test component for producing resources
public class ResourceProducer : MonoBehaviour
{
    private ShipResourceManager shipResourceManager = null;    
    [SerializeField] ShipResource shipResource = null;
    [SerializeField] int addAmount = 0;
    [SerializeField] bool addResource = false;
    void Start()
    {
        shipResourceManager = GetComponentInParent<ShipResourceManager>();
        if (shipResourceManager == null)
        {
            gameObject.SetActive(false);
            Debug.Log("No resource manager found in parent!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(addResource)
        {
            Debug.Log("added resource " + shipResource.name);
            shipResourceManager.addResource(shipResource, addAmount);
            addResource = false;
        }
    }
}
