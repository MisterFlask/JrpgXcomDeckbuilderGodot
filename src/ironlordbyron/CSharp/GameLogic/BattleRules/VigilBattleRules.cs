namespace GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic.BattleRules
{
    public static class VigilBattleRules
    {
        public static bool ShouldRetainVigilCardInHand(AbstractCard card)
        {
            return BattleHelpers.GetActiveVigilCard() == card;
        }
    }
}