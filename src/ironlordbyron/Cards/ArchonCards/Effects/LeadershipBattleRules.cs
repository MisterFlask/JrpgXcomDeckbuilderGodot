using System;
using System.Collections;

namespace Assets.CodeAssets.Cards.ArchonCards.Effects
{
    public static class LeadershipBattleRules
    {
        public static void PerformLeadershipAction(this AbstractCard card, Action action)
        {
            var ownerLevel = card.Owner.CurrentLevel;
            var leadershipApplies = (GameState.Instance.AllyUnitsInBattle.TrueForAll(
                allyUnit => allyUnit == card.Owner || allyUnit.CurrentLevel < card.Owner.CurrentLevel));
            
            if (leadershipApplies)
            {
                action();
            }
        }
    }
}