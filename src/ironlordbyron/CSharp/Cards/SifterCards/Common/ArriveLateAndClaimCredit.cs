namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.SifterCards.Common
{
    public class ArriveLateAndClaimCredit : AbstractCard
    {
        // Deal 4 damage.  Bounty.  Patient:  Then do it again.  Cost 1.
        public ArriveLateAndClaimCredit()
        {
            SetCommonCardAttributes("Arrive Late And Claim Credit", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 1);
            BaseDamage = 4;
            DamageModifiers.Add(BountyDamageModifier.GetBountyDamageModifier()); //todo
            ProtoSprite =
                ProtoGameSprite.ArchonIcon("conquerer");
        }

        public override string DescriptionInner()
        {
            return $"Dead {DisplayedDamage()} damage.  Patient: Then do it again.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            this.Patient(() =>
            {
                Action_AttackTarget(target);
            });
        }

    }
}