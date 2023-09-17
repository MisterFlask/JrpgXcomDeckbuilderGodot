using System.Collections.Generic;

public class StrategicDetails
{

    public Dictionary<string, int> SoldierClassNameToAmountOfDamageDealtToAmoebas { get; set; }
    public int StrategicTempHpStacks { get; set; }
    public int ColumbalHappiness { get; set; }
    public int FairyRitualsIgnored { get; set; }
}



public static class ColumbalHappiness
{
    private static int Happiness => GameState.Instance.StrategicDetails.ColumbalHappiness;
    public static void RegisterColumbalAttacked()
    {
        GameState.Instance.StrategicDetails.ColumbalHappiness -= 3;
    }

    public static void RegisterDayPassed()
    {
        GameState.Instance.StrategicDetails.ColumbalHappiness += 1;
    }


    public static bool IsShopOpen()
    {
        return GameState.Instance.StrategicDetails.ColumbalHappiness > 0;
    }

    public static float GetShopPriceMultiplier()
    {
        if (Happiness > 10)
        {
            return .8f;
        }
        if (Happiness < 4)
        {
            return 1.2f;
        }
        return 1f;
    }
}