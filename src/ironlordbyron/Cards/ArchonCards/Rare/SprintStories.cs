using System.Collections;

namespace Assets.CodeAssets.Cards.ArchonCards.Rare
{
    public class SprintStories : AbstractCard
    {
        // Draw two cards.  They cost 1 less this turn.  Cost 0.

        public SprintStories()
        {
            SetCommonCardAttributes("Sprint Stories", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);

            this.ProtoSprite =
                ProtoGameSprite.ArchonIcon("sprint");
        }

        public override string DescriptionInner()
        {
            return "Draw two cards.  They cost 1 less this turn.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().DrawCards(2, performOnCards: (cards) =>
            {
                foreach(var card in cards)
                {
                    card.RestOfTurnCostMod += -1;
                }
            });
        }
    }
}