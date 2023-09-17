using System.Linq;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic.BattleRules
{
    public static class BruteBattleRules
    {
        public static bool DoesBruteTrigger()
        {
            return GameState.Instance.Deck.Hand.Count() > 8;
        }

    }
}