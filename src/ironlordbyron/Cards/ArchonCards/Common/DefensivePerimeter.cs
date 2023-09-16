using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Common
{
    public class DefensivePerimeter : AbstractCard
    {
        public DefensivePerimeter()
        {
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            SetCommonCardAttributes("Defensive Perimeter", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 1,
                protoGameSprite: ProtoGameSprite.ArchonIcon("gate"));
        }

        /// apply 8 defense.  Leadership:  Each ally gains 2 Temporary Dexterity.
        public override string DescriptionInner()
        {
            return $"Apply 8 defense.  Leadership:  Each ally gains 8 Temporary HP.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyDefense(target, this.Owner, BaseDefenseValue);
            LeadershipBattleRules.PerformLeadershipAction(this, () =>
            {
                foreach(var ally in state().AllyUnitsInBattle)
                {
                    action().ApplyStatusEffect(ally, new TemporaryHpStatusEffect(), 8);
                }
            });
        }
    }


}