using UnityEngine;
using System.Collections.Generic;

public class Port : MonoBehaviour
{
    private int groups = 2;
    private int groupPoints = 50;
    SoulsManager soulsManager;
    List<List<Soul>> soulListList = new List<List<Soul>>();
    PortUI portUI;
    public bool embarkEnabled = false;
    public delegate void EmbarkDelegate();
    public EmbarkDelegate embarkDelegate;
    void Start()
    {
        Globals.instance.dayChangeDelegate += NewDay;
        soulsManager = GameObject.FindWithTag("Ship").GetComponent<SoulsManager>();
        portUI = GetComponent<PortUI>();
    }

    // This is where we arrive at port
    void NewDay()
    {
        // pause time
        Globals.instance.PauseTime();

        // call GameEventManager to start a new port story event

        // Generate two groups and rewards for them
        soulListList.Clear();

        for (int i = 0; i < groups; i++)
        {   
            soulListList.Add(soulsManager.GenerateSouls(0,groupPoints));
        }

        // Generate for sale resources
        // Generate for sale special items

        // Disable embark
        embarkEnabled = false;

        // Make port UI visible
        portUI.PortEventStart();

    }


    public void SelectGroup(int groupNumber)
    {
        // add rewards to ship resources and group souls to ship souls
        soulsManager.SetSoulList(soulListList[groupNumber]);
        embarkEnabled = true;
        portUI.checkUIStatus();

        //shipResourceManager something something
    }

    public void SelectDeal()
    {
        // add selected deal to inventory
    }

    public void Embark()
    {
        //close port ui
        //time moves normally now
        Globals.instance.UnPauseTime();
        embarkDelegate.Invoke();
    }
}
