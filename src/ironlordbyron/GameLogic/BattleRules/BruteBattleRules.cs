using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.GameLogic.BattleRules
{
    public static class BruteBattleRules
    {
        public static bool DoesBruteTrigger()
        {
            return GameState.Instance.Deck.Hand.Count() > 8;
        }

    }
}