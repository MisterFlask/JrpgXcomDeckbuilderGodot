namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.SifterCards.Uncommon
{
    public class ScopedOut : AbstractCard
    {
        // Deal 14 damage.  Cost 2.  Precision.
        // Patient:  Do it again.

        public ScopedOut()
        {
            SetCommonCardAttributes("Scoped Out", Rarity.UNCOMMON, TargetType.ENEMY, CardType.AttackCard, 1);
            DamageModifiers.Add(new PrecisionDamageModifier());

            ProtoSprite =
                ProtoGameSprite.ArchonIcon("targeting");
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage.  Patient: Do it again.";
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