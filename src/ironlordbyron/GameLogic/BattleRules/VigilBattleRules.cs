using Assets.CodeAssets.Cards;
using System.Collections;

namespace Assets.CodeAssets.GameLogic.BattleRules
{
    public static class VigilBattleRules
    {
        public static bool ShouldRetainVigilCardInHand(AbstractCard card)
        {
            return BattleHelpers.GetActiveVigilCard() == card;
        }
    }
}