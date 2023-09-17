using UnityEngine;
using System.Collections;
using System.Linq;

public class HighWindsMissionModifier : MissionModifier
{
    public override string Description()
    {
        return "Due to high winds, all cards of cost 0 in your deck will exhaust themselves at the beginning of combat.";
    }

    public override void OnMissionCombatBegins()
    {
        var cardsToExhaust = GameState.Instance.Deck.TotalDeckList.Where(item =>

            item.BaseEnergyCost() == 0

        );
        foreach(var card in cardsToExhaust)
        {
            ActionManager.Instance.ExhaustCard(card);
        }

    }

    public override int IncrementalMoney()
    {
        return 15;
    }
}
