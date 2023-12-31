﻿using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Rare
{
    public class ChimeOfLostWorlds : AbstractCard
    {
        // cost 1
        // 
        // Leadership:  Then give all allies +5 temporary strength and -4 stress.

        public ChimeOfLostWorlds()
        {
            SetCommonCardAttributes("Wail", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.AttackCard, 1, typeof(DiabolistSoldierClass));
            BaseDamage = 2;
        }

        public override string DescriptionInner()
        {
            return $"deal {BaseDamage} damage to 2 random enemies.  Apply 2 Weak and 2 Vulnerable to each. Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            for (int i = 0; i < 2; i++)
            {
                var targetedEnemy = state().EnemyUnitsInBattle.PickRandom();
                action().AttackUnitForDamage(targetedEnemy, Owner, BaseDamage, this);
                action().ApplyStatusEffect(targetedEnemy, new VulnerableStatusEffect(), 2);
                action().ApplyStatusEffect(targetedEnemy, new WeakenedStatusEffect(), 2);
            }
        }
    }
}