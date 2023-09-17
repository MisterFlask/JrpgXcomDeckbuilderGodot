using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;

namespace Assets.CodeAssets.Cards.ArchonCards.Starting
{
    public class CallTheRetreat : AbstractCard
    {
        public CallTheRetreat()
        {

            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass)); // todo: remove
            this.SetCommonCardAttributes(
                "F*** This, We Out",
                Rarity.RARE,
                TargetType.NO_TARGET_OR_SELF,
                CardType.SkillCard,
                1,
                protoGameSprite: ProtoGameSprite.ArchonIcon("dread")
                );

            this.BaseDefenseValue = 5;
        }

        public override string DescriptionInner()
        {
            return $"Flee the combat instantly.  ALL characters gain 10 stress.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach(var ally in state().AllyUnitsInBattle)
            {
                action().ApplyStress(ally, 10);
            }

            action().FleeCombat();
        }
    }
}