using Assets.CodeAssets.BattleEntities.StatusEffects;
using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;

namespace Assets.CodeAssets.Cards.HammerCards.Common
{
    public class FormerLineman : AbstractCard
    {
        // Apply 8 defense to an ally.  Deal 7 damage to all enemies attacking that ally; each gets -1 temporary strength.  Cost 2.

        public FormerLineman()
        {
            this.SoldierClassCardPools.Add(typeof(HammerSoldierClass));

            SetCommonCardAttributes("Former Lineman", Rarity.COMMON, TargetType.ALLY, CardType.AttackCard, 2, typeof(HammerSoldierClass));
            BaseDamage = 7;
            BaseDefenseValue = 8;
            ProtoSprite = ProtoGameSprite.HammerIcon("american-football-player");

        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} defense to an ally.  Deal {DisplayedDamage()} damage to all enemies attacking that ally; each gets -1 temporary strength.  Exert.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyDefenseToTarget(target);

            var enemiesAttackingTarget = state().GetUnitsAttackingUnit(target);
            foreach(var enemy in enemiesAttackingTarget)
            {
                Action_AttackTarget(enemy);
                Action_ApplyStatusEffectToTarget(new TemporaryStrengthStatusEffect(), -1, enemy);
            }
            
        }
    }
}