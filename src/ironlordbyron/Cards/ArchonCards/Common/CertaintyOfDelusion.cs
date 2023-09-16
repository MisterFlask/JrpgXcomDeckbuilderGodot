using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Common
{
    public class CertaintyOfDelusion : AbstractCard
    {

        public CertaintyOfDelusion()
        {
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            this.SetCommonCardAttributes(
                "Certainty Of Delusion",
                Rarity.COMMON,
                TargetType.NO_TARGET_OR_SELF,
                CardType.SkillCard,
                2,
                protoGameSprite: ProtoGameSprite.ArchonIcon("uprising")
                );
            this.BaseDefenseValue = 10;
            
        }

        public override string DescriptionInner()
        {
            return $"Apply 10 Stress Defense to ALL characters.  Apply {DisplayedDefense()} Defense to ALL characters.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach(var character in GameState.Instance.AllyUnitsInBattle)
            {
                action().ApplyStatusEffect(character, new StressDefenseStatusEffect(), 10);
                action().ApplyDefenseFromCard(this, character);
            }
        }
    }
}