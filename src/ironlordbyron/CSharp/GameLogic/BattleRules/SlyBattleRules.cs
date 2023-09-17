using System.Linq;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic.BattleRules
{
    public static class SlyBattleRules
    {

        public static bool DoesSlyTrigger()
        {
            return GameState.Instance.Deck.Hand.Count() < 3;
        }
    }
}