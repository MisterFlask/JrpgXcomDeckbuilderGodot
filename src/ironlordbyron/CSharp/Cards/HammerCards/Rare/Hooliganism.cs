using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.HammerCards.Rare
{
    public class Hooliganism : AbstractCard
    {
        // Deal 22 damage.  Gain 2 strength.  Discarded: Gain 1 strength.  Cost 2.  Exert.

        public Hooliganism()
        {
            SetCommonCardAttributes("Hooliganism", Rarity.RARE, TargetType.ENEMY, CardType.SkillCard, 2, typeof(HammerSoldierClass));
            BaseDamage = 22;
            ProtoSprite = ProtoGameSprite.HammerIcon("hoodie");

        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            Action_ApplyStatusEffectToTarget(new StrengthStatusEffect(), 2, Owner);
        }

        public override void OnManualDiscard()
        {
            Action_ApplyStatusEffectToTarget(new StrengthStatusEffect(), 1, Owner);
        }

        public override string DescriptionInner()
        {
            return $"Deal {BaseDamage} damage.  Gain 2 strength.  Discarded: Gain 1 strength.";
        }
    }
}