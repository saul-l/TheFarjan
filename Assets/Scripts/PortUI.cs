using UnityEngine;
using UnityEngine.UI;

public class PortUI : MonoBehaviour
{
    [SerializeField] Button embarkButton;
    [SerializeField] Button[] GroupButton;    
    [SerializeField] Button[] DealButton;
    [SerializeField] Canvas canvas;

    Port port;
    
    void Start()
    {
        port = GetComponent<Port>();
        canvas.enabled = false;
    }

    public void PortEventStart()
    {
        checkUIStatus();
        UIManagerSingleton.instance.TogglePortCanvas();
    }

    public void checkUIStatus()
    {
        embarkButton.interactable = port.embarkEnabled;
        
        foreach (Button button in GroupButton)
        {
            button.interactable = !port.embarkEnabled;
        }
        
        
    }
    public void SelectGroup(int groupNumber)
    {
       port.SelectGroup(groupNumber);

    }

    public void SelectDeal(int dealNumber)
    {
        //check if we have resources
        
    }

    public void Embark()
    {
        port.Embark();
        UIManagerSingleton.instance.TogglePortCanvas();
    }    
}
