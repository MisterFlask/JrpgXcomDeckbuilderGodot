﻿using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Common
{
    public class MeltItDown : AbstractCard
    {
        public MeltItDown()
        {
            SetCommonCardAttributes("Melt It Down", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);
            ProtoSprite = ProtoGameSprite.CogIcon("melting-ice-cube");
        }

        // Technocannibalize:  Gain 1 energy and 1 Charged.
        // Inferno: Then do it one more time.
        // Cost 0.
        public override string DescriptionInner()
        {
            return $"Technocannibalize:  Gain 1 energy and apply 1 Charged to target.  Inferno: Then do it one more time.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            CardAbilityProcs.Technocannibalize(this, () =>
            {
                CardAbilityProcs.GainEnergy(this, 1);
                action().ApplyStatusEffect(target, new ChargedStatusEffect());
            });
            CardAbilityProcs.Inferno(this, () =>
            {
                CardAbilityProcs.GainEnergy(this, 1);
                action().ApplyStatusEffect(target, new ChargedStatusEffect());
            });
        }
    }
}