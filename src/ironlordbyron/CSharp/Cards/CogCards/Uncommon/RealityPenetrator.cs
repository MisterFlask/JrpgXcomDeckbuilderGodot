namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.CogCards.Uncommon
{
    public class RealityPenetrator : AbstractCard
    {
        // Deal 8 damage.  Cost 3.  Buster.  Precision.  Refund 2.

        public RealityPenetrator()
        {
            SetCommonCardAttributes("Reality Penetrator", Rarity.UNCOMMON, TargetType.ENEMY, CardType.AttackCard, 3);
            BaseDamage = 8;
            Stickers.Add(new BasicAttackTargetSticker());
            DamageModifiers.Add(new BusterDamageModifier());
            DamageModifiers.Add(new PrecisionDamageModifier());
            ProtoSprite = ProtoGameSprite.CogIcon("vr-headset");

        }

        public override string DescriptionInner()
        {
            return "Refund 2.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            CardAbilityProcs.Refund(this, 2);
        }
    }
}