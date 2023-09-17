using System.Collections;

namespace Assets.CodeAssets.Cards.SifterCards.Common
{
    public class ArriveLateAndClaimCredit : AbstractCard
    {
        // Deal 4 damage.  Bounty.  Patient:  Then do it again.  Cost 1.
        public ArriveLateAndClaimCredit()
        {
            SetCommonCardAttributes("Arrive Late And Claim Credit", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 1);
            BaseDamage = 4;
            this.DamageModifiers.Add(BountyDamageModifier.GetBountyDamageModifier()); //todo
            this.ProtoSprite =
                ProtoGameSprite.ArchonIcon("conquerer");
        }

        public override string DescriptionInner()
        {
            return $"Dead {DisplayedDamage()} damage.  Patient: Then do it again.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            CardAbilityProcs.Patient(this, () =>
            {
                Action_AttackTarget(target);
            });
        }

    }
}