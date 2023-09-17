using Assets.CodeAssets.Cards;
using System.Collections;

namespace Assets.CodeAssets.GameLogic.BattleRules
{
    public static class LootBattleRules
    {

        public static void TriggerLootEvent(int money)
        {
            GameState.Instance.Credits += money;
        }
    }
}