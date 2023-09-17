using System.Collections;

namespace Assets.CodeAssets.Cards.CogCards.Uncommon
{
    public class BSLSK : AbstractCard
    {
        // BS-LSK
        // Apply 3 Weak to an enemy.
        // Technocannibalize:  Deal 7 damage to it. Cost 2.

        public BSLSK()
        {
            SetCommonCardAttributes("BSL-SK", Rarity.UNCOMMON, TargetType.ENEMY, CardType.AttackCard, 1);
            BaseDamage = 7;
            ProtoSprite = ProtoGameSprite.CogIcon("sea-serpent");

        }

        public override string DescriptionInner()
        {
            return $"Apply 3 Weak to the target.  Technocannibalize: Deal {DisplayedDamage()} damage to it.  Cost 1.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToTarget(new WeakenedStatusEffect(), 3, target);
            CardAbilityProcs.Technocannibalize(this, () =>
            {
                Action_AttackTarget(target);
            });
        }
    }
}