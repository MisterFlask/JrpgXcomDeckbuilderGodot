namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects
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
                damageBlob.Damage -= Stacks;
                Stacks = 0;
            }
            else
            {
                Stacks -= damageBlob.Damage;
                damageBlob.Damage = 0;
            }
        }
    }
}