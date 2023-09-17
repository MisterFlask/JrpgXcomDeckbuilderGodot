namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects
{
    public class MarkedStatusEffect : AbstractStatusEffect
    {
        public MarkedStatusEffect()
        {
            Name = "Marked";
        }

        public override string Description => $"Whenever this target is attacked, it takes 2 more damage (flat).  Decreases by 1 each turn.";

        public override int DamageDealtAddition()
        {
            return 2;
        }

        public override void OnTurnEnd()
        {
            action().TickDownStatusEffect<MarkedStatusEffect>(OwnerUnit);
        }
    }
}