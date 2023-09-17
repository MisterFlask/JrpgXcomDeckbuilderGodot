using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Common
{
    public class EyeForAnEye : AbstractCard
    {

        public EyeForAnEye()
        {
            SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            SetCommonCardAttributes(
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
            Action_Exhaust();
        }
    }
}