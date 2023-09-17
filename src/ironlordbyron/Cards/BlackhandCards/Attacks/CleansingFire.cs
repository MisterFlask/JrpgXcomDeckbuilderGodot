using System.Collections;

namespace Assets.CodeAssets.Cards.BlackhandCards.Attacks
{
    public class CleansingFire : AbstractCard
    {
        /// Deal 10 damage.  Lethal: Relieve 8 stress.
        
        public CleansingFire()
        {
            SetCommonCardAttributes("Cleansing Fire", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 2);
            this.DamageModifiers.Add(new SweeperDamageModifier());
            this.DamageModifiers.Add(new CleansingFireLethalTrigger());
            this.BaseDamage = 8;
            ProtoSprite = ProtoGameSprite.BlackhandIcon("vacuum-cleaner");

        }

        public override string DescriptionInner()
        {
            return "Deal 8 damage.  Lethal: Relieve 8 stress.  Sweeper.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().AttackWithCard(this, target);
        }
    }

    public class CleansingFireLethalTrigger : DamageModifier
    {
        public override bool SlayInner(AbstractCard damageSource, AbstractBattleUnit target)
        {
            action().ApplyStress(damageSource.Owner, -8);
            return true;
        }
    }
}