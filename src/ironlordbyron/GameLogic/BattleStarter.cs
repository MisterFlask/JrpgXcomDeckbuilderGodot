using UnityEngine;
using System.Collections;

public static class BattleStarter
{
    private static GameState state => ServiceLocator.GameState();
    public static void StartBattle(BattleScreenPrefab battleScreen)
    {
        foreach(var character in state.AllyUnitsInBattle)
        {
            foreach(var perk in character.Perks)
            {
                perk.PerformAtBeginningOfCombat(character);
            }
        }

        state.EnemyUnitsInBattle.ForEach((item) => item.CurrentIntents = item.GetNextIntents());
        // First, we kick off the player's turn.
    } 
}
