namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.CogCards.Uncommon
{
    public class EnergyWell : AbstractCard
    {
        public EnergyWell()
        {
            ProtoSprite = ProtoGameSprite.CogIcon("well");

        }

        // Technocannibalize:  Gain 1 energy and 1 Empowered.
        // Inferno: Then do it one more time.
        // Cost 0.
        public override string DescriptionInner()
        {
            return $"Technocannibalize:  Gain 1 energy and apply 1 Empowered to target.  Inferno: Then do it one more time.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            CardAbilityProcs.Technocannibalize(this, () =>
            {
                CardAbilityProcs.GainEnergy(this, 1);
                action().ApplyStatusEffect(target, new ChargedStatusEffect());
            });
            CardAbilityProcs.Inferno(this, () =>
            {
                CardAbilityProcs.GainEnergy(this, 1);
                action().ApplyStatusEffect(target, new ChargedStatusEffect());
            });
        }
    }
}