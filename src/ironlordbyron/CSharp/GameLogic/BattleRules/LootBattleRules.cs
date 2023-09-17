namespace GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic.BattleRules
{
    public static class LootBattleRules
    {

        public static void TriggerLootEvent(int money)
        {
            GameState.Instance.Credits += money;
        }
    }
}