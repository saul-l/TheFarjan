using UnityEngine;
using System.Collections.Generic;

public class Port : MonoBehaviour
{
    private int groups = 2;
    private int groupPoints = 50;
    private float groupRewardMultiplier = 1.2f;
    SoulsManager soulsManager;
    [SerializeField] ShipResourceManager shipResourceManager;

    [SerializeField] ShipResource[] groupRewardResourcePool;
    public List<List<Soul>> groupList = new List<List<Soul>>();
    public List<List<ShipResource>> groupRewardList = new List<List<ShipResource>>();
    public List<List<int>> groupRewardAmountList = new List<List<int>>();

    PortUI portUI;
    public bool embarkEnabled = false;
    public delegate void EmbarkDelegate();
    public EmbarkDelegate embarkDelegate;

    [SerializeField] ShipResource[] dealResourcePool;
    
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
        groupList.Clear();
        for (int i = 0; i < groups; i++)
        {   
            groupList.Add(soulsManager.GenerateSouls(0,groupPoints));
        }

        // Two random resource types as a reward is fine now, but eventually needs something better
        int differentRewardTypes = 2;
        
        groupRewardList.Clear();
        groupRewardAmountList.Clear();

        for(int i = 0;i < groups; i++)
        {
            int rewardValue = (int)Mathf.Round(groupPoints * groupRewardMultiplier);

            List<ShipResource> rewardList = new List<ShipResource>();
            List<int> rewardAmount = new List<int>();

            // Will only work with two groups properly
            for (int j = 0; j < differentRewardTypes; j++) 
            {
                int resourceAmount = 0;
                
                if (j == differentRewardTypes - 1)
                    resourceAmount = rewardValue;
                else
                { 
                    resourceAmount = Random.Range(0, rewardValue+1);
                    rewardValue -= resourceAmount;
                }
                Debug.Log("resourceAmount " + resourceAmount + " rewardValue " + rewardValue);
                rewardList.Add(groupRewardResourcePool[Random.Range(0, groupRewardResourcePool.Length)]);
                rewardAmount.Add(resourceAmount);
            }
            groupRewardList.Add(rewardList);
            groupRewardAmountList.Add(rewardAmount);
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
        soulsManager.SetSoulList(groupList[groupNumber]);
        for(int i = 0; i < groupRewardList.Count;i++)
        {
            shipResourceManager.AddResource(groupRewardList[groupNumber][i], groupRewardAmountList[groupNumber][i]);
        }
        groupRewardList.Clear();
        groupRewardAmountList.Clear();
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
