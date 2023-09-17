namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.CogCards.Rare
{
    public class WhereIsYourGodNow : AbstractCard
    {
        // Cost 3  Deal 20 damage to target, and apply 10 defense to ALL allies.  Buster, Slayer.

        public WhereIsYourGodNow()
        {
            SetCommonCardAttributes("Where Is Your God Now", Rarity.RARE, TargetType.ENEMY, CardType.AttackCard, 3);
            BaseDamage = 20;
            BaseDefenseValue = 10;
            DamageModifiers.Add(new BusterDamageModifier());
            DamageModifiers.Add(new SlayerDamageModifier());
            ProtoSprite = ProtoGameSprite.CogIcon("anubis");

        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage to target, and apply {DisplayedDefense()} block to ALL allies.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            Action_DefendAllAllies();
        }
    }
}