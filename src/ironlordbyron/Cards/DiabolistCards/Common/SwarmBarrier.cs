using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Common
{
    public class SwarmBarrier : AbstractCard
    {
        // apply 2 defense and 1 stress to ALL allies.  Startup: This gains +2 to defense for each other Swarm card in your deck.  Swarm.


        public SwarmBarrier()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Swarm Barrier", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.AttackCard, 1);
            BaseDefenseValue = 2;
            this.CardTags.Add(BattleCardTags.SWARM);
            ProtoSprite = ProtoGameSprite.DiabolistIcon("shield-bounces");
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} and 1 stress to ALL allies.  Swarm.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach(var ally in state().AllyUnitsInBattle)
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
                    this.BaseDefenseValue += 2;
                }
            }
        }

    }
}