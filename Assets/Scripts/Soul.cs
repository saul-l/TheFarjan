// Souls aka crew + passengers

// Daily ship food consumption in buffet is calculated using combined dailyFoodChange of all souls
// If there isn't enough food to consume there is increasing risk of souls dying
// Not reaching food consumption quota also leads to decrease in joy

// Souls can also "consume" joy, aka you need to produce joy in Night Club or Tax Free to not lose Joy

// Souls can consume or 
public class Soul
{
    public int crewMemberStatus = 0; // modifier for some values
    public int dailyFoodChange = -1;
    public int dailyJoyChange = -1;
    public int dailyOrderChange = 0;
    public int dailySuppliesChange = 0;
    public string soulName = string.Empty;    
}
