namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.SifterCards.Rare
{
    public class EarnMySalary : AbstractCard
    {
        // Deal 30 damage.  Precision.  Bounty.  Cost 3.
        // Hoard 4.

        public EarnMySalary()
        {
            SetCommonCardAttributes("Earn My Salary", Rarity.RARE, TargetType.ENEMY, CardType.AttackCard, 3);
            DamageModifiers.Add(new PrecisionDamageModifier());
            DamageModifiers.Add(BountyDamageModifier.GetBountyDamageModifier());
            Stickers.Add(new GildedCardSticker(4));
            BaseDamage = 30;
            ProtoSprite = ProtoGameSprite.CogIcon("farmer");

        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} to target.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
        }
    }
}