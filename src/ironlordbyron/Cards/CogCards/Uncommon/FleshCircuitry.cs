using System.Collections;

namespace Assets.CodeAssets.Cards.CogCards.Uncommon
{
    public class FleshCircuitry : AbstractCard
    {

        public FleshCircuitry()
        {
            SetCommonCardAttributes("Fleshy Circuitry", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1);
            Stickers.Add(new ExhaustCardSticker());
            ProtoSprite = ProtoGameSprite.CogIcon("circuitry");

        }

        public override string DescriptionInner()
        {
            return "Gain 2 strength.  Technocannibalize: Do it again.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToOwner(new StrengthStatusEffect(), 2);
            CardAbilityProcs.Technocannibalize(this, () =>
            {
                Action_ApplyStatusEffectToOwner(new StrengthStatusEffect(), 2);
            });
        }
    }
}