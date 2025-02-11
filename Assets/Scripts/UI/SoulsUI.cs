using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoulsUI : MonoBehaviour
{
    [SerializeField] GameObject scrollViewContent;
    [SerializeField] GameObject soulPanelPrefab;
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
    }

    // Update is called once per frame
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
        Debug.Log("sizedelta " + scrollViewContentRT.sizeDelta);
        scrollViewContentRT.sizeDelta = new Vector2(scrollViewContentRT.sizeDelta.x, scrollViewContentStartHeight + panelYSeparation * -yOff);
        Debug.Log("sizedelta " + scrollViewContentRT.sizeDelta);
    }

    public void ToggleVisibility(bool visibility)
    {        
        canvas.enabled = visibility;
    }
    
}
