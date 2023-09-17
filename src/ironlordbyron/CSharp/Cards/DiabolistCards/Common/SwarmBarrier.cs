using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Common
{
    public class SwarmBarrier : AbstractCard
    {
        // apply 2 defense and 1 stress to ALL allies.  Startup: This gains +2 to defense for each other Swarm card in your deck.  Swarm.


        public SwarmBarrier()
        {
            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            SetCommonCardAttributes("Swarm Barrier", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.AttackCard, 1);
            BaseDefenseValue = 2;
            CardTags.Add(BattleCardTags.SWARM);
            ProtoSprite = ProtoGameSprite.DiabolistIcon("shield-bounces");
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} and 1 stress to ALL allies.  Swarm.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach (var ally in state().AllyUnitsInBattle)
            {
                action().ApplyDefense(ally, Owner, BaseDefenseValue);
            }
        }

        public override void OnStartup()
        {
            foreach (var card in state().Deck.TotalDeckList)
            {
                if (card.CardType == CardType.ErosionCard)
                {
                    BaseDefenseValue += 2;
                }
            }
        }

    }
}