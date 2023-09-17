using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using System.Collections;

namespace Assets.CodeAssets.Cards.HammerCards.Common
{
    public class OnMySignal : AbstractCard
    {
        // Cost 0.  Apply 2 Marked.

        public OnMySignal()
        {
            this.SoldierClassCardPools.Add(typeof(HammerSoldierClass));
            ProtoSprite = ProtoGameSprite.HammerIcon("crosshair");

            SetCommonCardAttributes("On My Signal", Rarity.COMMON, TargetType.ENEMY, CardType.SkillCard, 1, typeof(HammerSoldierClass));
        }

        public override string DescriptionInner()
        {
            return "Apply 2 Marked";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToTarget(new MarkedStatusEffect(), 2, target);
        }
    }
}