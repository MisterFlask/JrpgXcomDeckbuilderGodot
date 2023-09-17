using System.Collections;
using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;

namespace Assets.CodeAssets.Cards.HammerCards.Common
{
    public class PointDefenseSystem : AbstractCard
    {
        // Apply 5 defense.  Cost 0.  Draw a card.

        public PointDefenseSystem()
        {
            this.SoldierClassCardPools.Add(typeof(HammerSoldierClass));

            SetCommonCardAttributes("Point Defense System", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 0);
            BaseDefenseValue = 5; 
            ProtoSprite = ProtoGameSprite.HammerIcon("guards");

        }

        public override string DescriptionInner()
        {
            return $"Apply {BaseDefenseValue} defense.  Draw a card.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyDefenseToTarget(target);
            action().DrawCards(1);
        }
    }
}