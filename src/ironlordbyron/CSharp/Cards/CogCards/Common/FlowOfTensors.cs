namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.CogCards.Common
{
    public class FlowOfTensors : AbstractCard
    {
        // Add two Autocannons to your hand.  Discharge: Gain a data point and Exhaust.  Cost 1.

        public FlowOfTensors()
        {
            SetCommonCardAttributes("Flow of Tensors", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
            ProtoSprite = ProtoGameSprite.CogIcon("movement-sensor");
        }

        public override string DescriptionInner()
        {
            return "Add two Autocannons to your hand.  Discharge: Gain a data point and Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().CreateCardToHand(new AutocannonSentry());
            action().CreateCardToHand(new AutocannonSentry());

            CardAbilityProcs.Discharge(this, () =>
            {
                CardAbilityProcs.GainDataPoints(this, 1);
                Action_Exhaust();
            });
        }
    }
}