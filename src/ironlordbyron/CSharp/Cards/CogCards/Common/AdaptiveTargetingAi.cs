using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.CogCards.Special;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.CogCards.Common
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
            action().ApplyStatusEffect(Owner, new StrengthStatusEffect());
            action().CreateCardToHand(new AutocannonSentry());
            action().CreateCardToHand(new AutocannonSentry());
        }
    }
}