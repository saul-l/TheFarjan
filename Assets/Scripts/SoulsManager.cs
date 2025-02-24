using UnityEngine;
using System.Collections.Generic;

public class SoulsManager : MonoBehaviour
{
    [SerializeField] private List<Soul> soulList = new List<Soul>();
    [SerializeField] private int dailyFoodChange = 0;
    [SerializeField] GameObject soulsUICanvasPrefab;

    private SoulsUI soulsUI;
    private Canvas soulsCanvas;

    private int startingSouls = 50;
    private SoulNames soulNames = new SoulNames();
    void Start()
    {

        // Create 5 crewmembers and 45 passengers at start of the game

        soulList = GenerateSouls(0, startingSouls);

        GetDailyFoodChange();

        GameObject soulsUICanvasInstance = GameObject.Instantiate(soulsUICanvasPrefab);
        soulsUI = soulsUICanvasInstance.GetComponent<SoulsUI>();
        soulsCanvas = soulsUICanvasInstance.GetComponent<Canvas>();
        soulsUI.UpdateSoulsUI(soulList);
    }

    public List<Soul> GenerateSouls(int crew, int passengers)
    {
        List<Soul> generatedSouls = new List<Soul>();
        for (int i = 0; i < startingSouls; i++)
        {
            Soul newSoul = new Soul();

            if (Random.Range(0, 2) == 0)
                newSoul.soulName = soulNames.finnishFirstNames[Random.Range(0, soulNames.finnishFirstNames.Length)] + " " + soulNames.finnishSurnames[Random.Range(0, soulNames.finnishSurnames.Length)];
            else
                newSoul.soulName = soulNames.swedishFirstNames[Random.Range(0, soulNames.swedishFirstNames.Length)] + " " + soulNames.swedishSurnames[Random.Range(0, soulNames.swedishSurnames.Length)];

            generatedSouls.Add(newSoul);
        }
        return generatedSouls;
    }

    public void SetSoulList(List<Soul> sList)
    {
        soulList.Clear();
        soulList = sList;
        GetDailyFoodChange();
        soulsUI.UpdateSoulsUI(soulList);
    }
    public int GetTotalSouls()
    {
        int totalSouls = soulList.Count;
        return totalSouls;
    }
    public int GetDailyFoodChange()
    {
        int totalFoodConsumption = 0;
        foreach (Soul soul in soulList)
        {
            totalFoodConsumption+=soul.dailyFoodChange;
        }
        dailyFoodChange = totalFoodConsumption;
        return totalFoodConsumption;
    }

    public int[] GetFoodConsumptionArray()
    {
        int[] foodComsumptionArray = new int[soulList.Count];
        for(int i = 0; i < soulList.Count; i++)
        {
            foodComsumptionArray[i] = soulList[i].dailyFoodChange;
        }
        return foodComsumptionArray;
    }

    public int GetAmountOfSouls()
    {
        return soulList.Count;
    }
}