using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Rare
{
    public class CallTheRetreat : AbstractCard
    {
        public CallTheRetreat()
        {

            SoldierClassCardPools.Add(typeof(ArchonSoldierClass)); // todo: remove
            SetCommonCardAttributes(
                "F*** This, We Out",
                Rarity.RARE,
                TargetType.NO_TARGET_OR_SELF,
                CardType.SkillCard,
                1,
                protoGameSprite: ProtoGameSprite.ArchonIcon("dread")
                );

            BaseDefenseValue = 5;
        }

        public override string DescriptionInner()
        {
            return $"Flee the combat instantly.  ALL characters gain 10 stress.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach (var ally in state().AllyUnitsInBattle)
            {
                action().ApplyStress(ally, 10);
            }

            action().FleeCombat();
        }
    }
}