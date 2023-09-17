using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.HammerCards.Common
{
    public class PowerFist : AbstractCard
    {
        // Deal 5 damage, twice.

        public PowerFist()
        {
            SetCommonCardAttributes("Power Fist", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 1, typeof(HammerSoldierClass));
            BaseDamage = 5;
            ProtoSprite = ProtoGameSprite.HammerIcon("mailed-fist");

        }

        public override string DescriptionInner()
        {
            return $"Deal {BaseDamage}, twice";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            Action_AttackTarget(target);
        }
    }
}