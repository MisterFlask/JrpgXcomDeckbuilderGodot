using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Common
{
    public class EyeForAnEye : AbstractCard
    {

        public EyeForAnEye()
        {
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            this.SetCommonCardAttributes(
                "Eye for an Eye",
                Rarity.COMMON,
                TargetType.ALLY,
                CardType.SkillCard,
                1,
                protoGameSprite: ProtoGameSprite.ArchonIcon("eyepatch")
                );
        }

        private int StacksOfRetaliateToApply = 3;

        public override string DescriptionInner()
        {
            return $"Apply {StacksOfRetaliateToApply} Retaliate. Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new RetaliateStatusEffect(), StacksOfRetaliateToApply);
            this.Action_Exhaust();
        }
    }
}