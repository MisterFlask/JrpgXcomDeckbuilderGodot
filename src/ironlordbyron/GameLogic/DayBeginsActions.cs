using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayBeginsActions : MonoBehaviour
{

    public static void ApplyTriggers()
    {
        foreach(var character in GameState.Instance.PersistentCharacterRoster)
        {
            foreach(var perk in character.Perks)
            {
                perk.PerformAtBeginningOfNewDay(character);
            }
        }
    }

    public void StartANewDay()
    {
        GameState.Instance.Day++;
        // RotateMissions(); nah, not doing this anymore
        HealAndDestressForTheDay();
        ApplyTriggers();
    }

    public void HealAndDestressForTheDay()
    {
        GameState.Instance.PersistentCharacterRoster.ForEach(character =>
        {
            character.Heal(character.PerDayHealingRate);
            character.ModifyStress(character.PerDayStressHealingRate * -1);
        });
    }

}

