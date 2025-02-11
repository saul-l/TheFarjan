using UnityEngine;
using System.Collections.Generic;

public class SoulsManager : MonoBehaviour
{
    [SerializeField] private List<Soul> soulList = new List<Soul>();
    [SerializeField] private int dailyFoodChange = 0;
    [SerializeField] GameObject soulsUICanvasPrefab;
    
    private SoulsUI soulsUI;

    private int startingSouls = 50;

    void Start()
    {
         SoulNames soulNames = new SoulNames();
        // Create 5 crewmembers and 45 passengers at start of the game
        for(int i = 0; i< startingSouls;i++)
        { 
            Soul newSoul = new Soul();

            if(Random.Range(0, 2) == 0)
                newSoul.soulName = soulNames.finnishFirstNames[Random.Range(0, soulNames.finnishFirstNames.Length)] + " " + soulNames.finnishSurnames[Random.Range(0, soulNames.finnishSurnames.Length)];
            else
                newSoul.soulName = soulNames.swedishFirstNames[Random.Range(0, soulNames.swedishFirstNames.Length)] + " " + soulNames.swedishSurnames[Random.Range(0, soulNames.swedishSurnames.Length)];

            soulList.Add(newSoul);
        }

        GetShipDailyFoodChange();

        GameObject soulsUICanvasInstance = GameObject.Instantiate(soulsUICanvasPrefab);
        soulsUI = soulsUICanvasInstance.GetComponent<SoulsUI>();
        soulsUI.UpdateSoulsUI(soulList);
    }


    int GetShipDailyFoodChange()
    {
        int totalFoodConsumption = 0;
        foreach (Soul soul in soulList)
        {
            totalFoodConsumption+=soul.dailyFoodChange;
        }
        dailyFoodChange = totalFoodConsumption;
        return totalFoodConsumption;
    }
}