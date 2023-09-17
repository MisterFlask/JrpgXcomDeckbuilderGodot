using Assets.CodeAssets.Cards.CogCards.Special;
using System.Collections;

namespace Assets.CodeAssets.Cards.CogCards.Common
{
    public class AdaptiveTargetingAi : AbstractCard
    {
        // Add two Autosentries to your hand.  Draw a card.  Cost 1.

        public AdaptiveTargetingAi()
        {
            SetCommonCardAttributes("Adaptive Targeting AI", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 2);
            ProtoSprite = ProtoGameSprite.CogIcon("artificial-intelligence");
        }

        public override string DescriptionInner()
        {
            return "Gain 1 strength.  Add two Autocannon Sentries to your hand.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(this.Owner, new StrengthStatusEffect());
            action().CreateCardToHand(new AutocannonSentry());
            action().CreateCardToHand(new AutocannonSentry());
        }
    }
}