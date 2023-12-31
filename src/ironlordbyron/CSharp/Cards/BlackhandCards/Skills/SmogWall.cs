﻿using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.BlackhandCards.Skills
{
    public class SmogWall : AbstractCard
    {
        // Apply 8 block to an ally.  Apply an amount of fumes equal to that character's block to a random enemy.

        public SmogWall()
        {
            SetCommonCardAttributes("Smog Wall", Rarity.UNCOMMON, TargetType.ALLY, CardType.SkillCard, 1, typeof(BlackhandSoldierClass));
            BaseDefenseValue = 8;
            ProtoSprite = ProtoGameSprite.BlackhandIcon("defensive-wall");

        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} block to an ally.  Apply an amount of fumes equal to that character's block to a random enemy.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyDefense(target, Owner, BaseDefenseValue);
            action().PushActionToBack("SmogWall", () =>
            {
                var fumesToApply = target.CurrentBlock;
                action().ApplyStatusEffect(CardTargeting.RandomTargetableEnemy(), new FumesStatusEffect(), fumesToApply);
            });
        }
    }
}