namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects
{
    public class LogisticalSupportStatusEffect : AbstractStatusEffect
    {

        public static string NAME = "Logistics";

        public LogisticalSupportStatusEffect()
        {
            Name = NAME;
            StatusLocalityType = StatusLocalityType.GLOBAL;
            StatusPolarityType = StatusPolarityType.BUFF;
        }

        public override string Description => $"The next {DisplayedStacks()} times that you play a card, play it again.";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit target, bool ownedByMe)
        {
            action().EvokeCardEffect(cardPlayed, target);

        }
    }
}