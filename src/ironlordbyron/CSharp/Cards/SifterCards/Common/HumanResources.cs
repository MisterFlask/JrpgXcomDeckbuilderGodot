﻿using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.StatusEffects;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.SifterCards.Common
{
    public class HumanResources : AbstractCard
    {
        public HumanResources()
        {
            SetCommonCardAttributes("Human Resources", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 1);

            ProtoSprite =
                ProtoGameSprite.ArchonIcon("human-pyramid");
        }

        // apply 2 Charged to an ally.  Hoard 5.
        public override string DescriptionInner()
        {
            return $"Apply 2 Charged to an ally.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToTarget(new ChargedStatusEffect(), 2, target);
        }
    }
}