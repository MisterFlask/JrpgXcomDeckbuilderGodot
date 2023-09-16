using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Rare
{
    public class ImprovisedTechnomancy : AbstractCard
    {
        // The first Created card you play each turn costs 1 less.

        public ImprovisedTechnomancy()
        {
            SetCommonCardAttributes("Improvised Technomancy", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 2);
            ProtoSprite = ProtoGameSprite.CogIcon("magick-trick");
        }

        public override string DescriptionInner()
        {
            return "The first created card you play each turn costs 1 less.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToOwner(new ImprovisedTechnomancyStatusEffect(), 1);
        }
    }


    public class ImprovisedTechnomancyStatusEffect : AbstractStatusEffect
    {
        public ImprovisedTechnomancyStatusEffect()
        {
            Name = "Improvised Technomancy";
            ProtoSprite = ProtoGameSprite.CogIcon("magick-trick");
        }

        public override string Description => $"The first {DisplayedStacks()} Created cards you play each turn cost 1 less.";

        private int numberCreatedCardsPlayedThisTurn = 0;

        public override int CardCostModifier(AbstractCard card, bool ownedByMe)
        {
            if (card.WasCreated && numberCreatedCardsPlayedThisTurn < Stacks)
            {
                return -1;
            }
            return 0;
        }

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool cardIsOwnedByMe)
        {
            if (cardPlayed.WasCreated)
            {
                numberCreatedCardsPlayedThisTurn ++ ;
            }
        }

        public override void OnTurnStart()
        {
            numberCreatedCardsPlayedThisTurn = 0;
        }
    }
}