using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Rare
{
    public class SelfSupervisedLearning : AbstractCard
    {
        // Gain 5 Data Points and 30 stress.  Gain 1 strength for each Erosion in your Draw and Discard pile.  Cost 0.
        public SelfSupervisedLearning()
        {
            SetCommonCardAttributes("Self-Supervised Learning", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);
            ProtoSprite = ProtoGameSprite.CogIcon("mirror-mirror");
        }

        public override string DescriptionInner()
        {
            return $"Gain 5 Data Points and 30 stress.  Gain 1 strength for each non-exhausted Erosion in your deck.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            CardAbilityProcs.GainDataPoints(this, 5);
            action().ApplyStress(target, 30);
            var erosions = state().Deck.TotalDeckList.Where(item => item.CardType == CardType.ErosionCard);
            var numberOfErosions = erosions.Count();
            action().ApplyStatusEffect(target, new StrengthStatusEffect(), numberOfErosions);
            Action_Exhaust();
        }
    }
}