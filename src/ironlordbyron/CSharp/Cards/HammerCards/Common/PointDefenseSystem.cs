using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.HammerCards.Common
{
    public class PointDefenseSystem : AbstractCard
    {
        // Apply 5 defense.  Cost 0.  Draw a card.

        public PointDefenseSystem()
        {
            SoldierClassCardPools.Add(typeof(HammerSoldierClass));

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