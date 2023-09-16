using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards.Common
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