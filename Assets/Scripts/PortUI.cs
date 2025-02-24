using UnityEngine;
using UnityEngine.UI;

public class PortUI : MonoBehaviour
{
    [SerializeField] Button embarkButton;
    [SerializeField] Button[] GroupButton;    
    [SerializeField] Button[] DealButton;
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasManager canvasManager = null;
    Port port;
    
    void Awake()
    {
        canvas.enabled = false;
        Debug.Log("canvas " + canvas.name);
        canvasManager.AddCanvasToList(canvas);
    }
    void Start()
    {
        port = GetComponent<Port>();
    }

    public void PortEventStart()
    {
        checkUIStatus();
        EnableCanvas();
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
    public void EnableCanvas()
    {
        canvasManager.EnableCanvas(canvas);
    }
    public void DisableCanvas()
    {
        canvasManager.DisableCanvas(canvas);
    }
    public void Embark()
    {
        port.Embark();
        DisableCanvas();
    }    
}
