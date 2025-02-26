using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoulsUI : MonoBehaviour
{
    [SerializeField] GameObject scrollViewContent;
    [SerializeField] GameObject soulPanelPrefab;
    [SerializeField] Button closeButton;
    [SerializeField] CanvasManager canvasManager;
    GameObject[] soulPanelInstances = new GameObject[0];
    Canvas canvas;
    
    const float scrollViewContentStartHeight = 450;
    const float panelXStartPos = 300f;
    const float panelYStartPos = -220f;
    const float panelXSeparation = 550f;
    const float panelYSeparation = -420f;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        canvasManager.AddCanvasToList(canvas);
        closeButton.onClick.AddListener(()=>canvasManager.DisableCanvas(canvas));
    }

    public void EnableCanvas()
    {
        canvasManager.EnableCanvas(canvas);
    }
    public void UpdateSoulsUI(List<Soul> soulList)
    {
        for (int i = 0; i < soulPanelInstances.Length; i++)
        {
            Destroy(soulPanelInstances[i]);
        }

        if (soulPanelInstances.Length != soulList.Count)                       
            soulPanelInstances = new GameObject[soulList.Count];

        int yOff = 0;
        int xOff = 0;
        for(int i = 0;i < soulPanelInstances.Length; i++)
        {
            soulPanelInstances[i] = Instantiate(soulPanelPrefab);
            soulPanelInstances[i].transform.SetParent(scrollViewContent.transform, false);
            soulPanelInstances[i].transform.localPosition = new Vector3(panelXStartPos + xOff * panelXSeparation, panelYStartPos + yOff * panelYSeparation, 0f);

            //give em values here

            SoulPanelUI sPUI = soulPanelInstances[i].GetComponent<SoulPanelUI>();
            sPUI.characterName.text = soulList[i].soulName;


            xOff++;
            if(xOff>2)
                {
                    yOff++;
                    xOff = 0;
                }
        }

        RectTransform scrollViewContentRT = scrollViewContent.GetComponent<RectTransform>();
        scrollViewContentRT.sizeDelta = new Vector2(scrollViewContentRT.sizeDelta.x, scrollViewContentStartHeight + panelYSeparation * -yOff);
    }    
}
