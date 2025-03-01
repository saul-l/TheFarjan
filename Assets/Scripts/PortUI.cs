using TMPro;
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

    void Start()
    {
        port = gameObject.GetComponent<Port>();
        canvas.enabled = false;
        canvasManager.AddCanvasToList(canvas);
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

        for (int i = 0; i < GroupButton.Length; i++)
        {
            string buttonText = "";
            buttonText += "Passengers " + port.groupList[i].Count + System.Environment.NewLine;
            if (port.groupRewardList.Count != 0)
            {
                for (int j = 0; j < port.groupRewardList[i].Count; j++)
                {
                    buttonText += port.groupRewardList[i][j].resourceName + " " + port.groupRewardAmountList[i][j] + System.Environment.NewLine;
                }
            }
            ChangeButtonText(GroupButton[i], buttonText);
        }
        /*
        for (int i = 0;i < DealButton.Length; i++)
        {

        }
        */
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
    
    private void ChangeButtonText(Button button, string text)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        buttonText.text = text;
    }
}
