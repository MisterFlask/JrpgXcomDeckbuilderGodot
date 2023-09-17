﻿using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using System.Collections;

namespace Assets.CodeAssets.Cards.DiabolistCards.Common
{
    public class HarnessTheVoid : AbstractCard
    {
        // Gain 10 Temp HP
        // Sacrifice: ALL allies gain 1 strength.

        public HarnessTheVoid()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Harness the Void", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 2);
            ProtoSprite = ProtoGameSprite.DiabolistIcon("pentagram-rose");
            MagicNumber = 10;
        }


        public override string DescriptionInner()
        {
            return $"Apply {MagicNumber} temp HP.  Sacrifice: exhaust, and ALL allies gain 1 strength.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new TemporaryHpStatusEffect(), MagicNumber);

            this.Sacrifice(() =>
            {
                this.Action_Exhaust();
                foreach (var ally in state().AllyUnitsInBattle)
                {
                    action().ApplyStatusEffect(target, new StrengthStatusEffect(), 1);
                }
            });
        }

    }
}