namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Augmentations
{
    public class EnergyAmpAugment
    {
        // todo
    }

    public class PerEnergyCostDamageModifier : DamageModifier
    {
        int multiplier;
        int baseDamageIncrease;
        public PerEnergyCostDamageModifier(int multiplier, int baseDamageIncrease)
        {
            this.multiplier = multiplier;
            this.baseDamageIncrease = baseDamageIncrease;
        }

        public override int GetIncrementalDamageAddition(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
        {
            return damageSource.BaseEnergyCost() * 1;
        }
    }
}
