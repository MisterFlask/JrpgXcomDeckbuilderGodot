namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects
{
    public class BindingStatusEffect : AbstractStatusEffect
    {
        public BindingStatusEffect()
        {
            Name = "Binding";
        }
        public override string Description => "At the end of your turn, if a unit's Binding is greater than its HP, it dies.  Otherwise, it loses half of its Binding.";

        public override void OnTurnEnd()
        {
            if (Stacks > OwnerUnit.CurrentHp)
            {
                action().KillUnit(OwnerUnit);
            }
            else
            {
                action().ApplyStatusEffect(OwnerUnit, new BindingStatusEffect(), (int)(-0.5 * Stacks));
            }
        }

    }
}