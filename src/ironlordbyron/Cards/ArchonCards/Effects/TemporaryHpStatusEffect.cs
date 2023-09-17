using System.Collections;

namespace Assets.CodeAssets.Cards.ArchonCards.Effects
{
    public class TemporaryHpStatusEffect : AbstractStatusEffect
    {
        public TemporaryHpStatusEffect()
        {
            Name = "Temporary HP";
        }

        public override string Description => "If you would lose HP, lose this instead.";

        public override void ModifyPostBlockDamageTaken(DamageBlob damageBlob)
        {
            if (damageBlob.Damage >= Stacks)
            {
                damageBlob.Damage -= this.Stacks;
                this.Stacks = 0;
            }
            else
            {
                this.Stacks -= damageBlob.Damage;
                damageBlob.Damage = 0;
            }
        }
    }
}