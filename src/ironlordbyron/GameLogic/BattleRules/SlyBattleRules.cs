using System.Collections;
using System.Linq;

namespace Assets.CodeAssets.GameLogic.BattleRules
{
    public static class SlyBattleRules
    {

        public static bool DoesSlyTrigger()
        {
            return GameState.Instance.Deck.Hand.Count() < 3;
        }
    }
}