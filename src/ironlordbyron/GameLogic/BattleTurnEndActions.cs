using System.Collections;
using System;
using System.Linq;

public class BattleTurnEndActions
{
    ActionManager actionManager => ServiceLocator.GetActionManager();
    GameState gameState => ServiceLocator.GameState();

    internal void EndTurn()
    {
        actionManager.DiscardHand();

        gameState.AllyUnitsInBattle.ForEachCreateActionToBack("OnTurnEnd_Ally", item => item.OnTurnEnd());

        gameState.EnemyUnitsInBattle.ForEachCreateActionToBack("OnTurnEnd_Enemy", item => item.OnTurnEnd());

        gameState.EnemyUnitsInBattle.ForEachCreateActionToBack("Intent Execution", item => item.ExecuteOnIntentIfAvailable());

        GameState.Instance.Deck.TotalDeckList.ForEachCreateActionToBack("reset rest of turn cost mods", item =>
        {
            item.RestOfTurnCostMod = 0; // these get reset to 0 at the beginning of each turn.
        });

        StartNewTurn();
    }

    private void StartNewTurn()
    {
        GameState.Instance.NumCardsPlayedThisTurn = 0;
        ServiceLocator.GameState().BattleTurn++;

        ServiceLocator.GetActionManager().PushActionToBack("OnTurnStart(allies)", () =>
        {
            gameState.AllyUnitsInBattle.ForEach(item => item.OnTurnStart());
        });
        ServiceLocator.GetActionManager().PushActionToBack("OnTurnStart(enemies)", () =>
        {
            gameState.EnemyUnitsInBattle.ForEach(item => item.OnTurnStart());
        });
        ServiceLocator.GetActionManager().PushActionToBack("Draw cards for turn", () => actionManager.DrawCards(5));
        ServiceLocator.GetActionManager().PushActionToBack("Set energy", () =>
        {
            ServiceLocator.GameState().energy = ServiceLocator.GameState().maxEnergy;
        });
        ServiceLocator.GetActionManager().PushActionToBack("check is battle over", () => ActionManager.Instance.CheckIsBattleOver());
    }
}
