using System.Collections;

namespace Assets.CodeAssets.Cards.ArchonCards.Effects
{
    public class BindingStatusEffect : AbstractStatusEffect
    {
        public BindingStatusEffect()
        {
            this.Name = "Binding";
        }
        public override string Description => "At the end of your turn, if a unit's Binding is greater than its HP, it dies.  Otherwise, it loses half of its Binding.";

        public override void OnTurnEnd()
        {
            if (this.Stacks > this.OwnerUnit.CurrentHp)
            {
                action().KillUnit(this.OwnerUnit);
            }
            else
            {
                action().ApplyStatusEffect(this.OwnerUnit, new BindingStatusEffect(), (int) (-0.5 * this.Stacks));
            }
        }

    }
}