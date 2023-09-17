﻿using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.BlackhandCards.Skills
{
    public class FireShelter : AbstractCard
    {
        // Apply 4 Temporary Thorns and 20 defense to the target.
        // Cost 2.

        public FireShelter()
        {
            SetCommonCardAttributes("Fire Shelter", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 2, typeof(BlackhandSoldierClass));
            ProtoSprite = ProtoGameSprite.BlackhandIcon("dog-house");
        }

        public override string DescriptionInner()
        {
            return $"Apply 4 Temporary Thorns and {DisplayedDefense()} defense to ALL allies.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach (var ally in state().AllyUnitsInBattle)
            {
                action().ApplyStatusEffect(ally, new TemporaryThorns(), 4);
                action().ApplyDefense(ally, Owner, BaseDefenseValue);
            }
        }
    }
}