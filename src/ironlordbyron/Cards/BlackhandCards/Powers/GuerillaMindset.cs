using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Powers
{
    public class GuerillaMindset : AbstractCard
    {
        public GuerillaMindset()
        {
            this.SetCommonCardAttributes("Guerilla Mindset", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1);
            ProtoSprite = ProtoGameSprite.BlackhandIcon("hidden");
        }

        public override string DescriptionInner()
        {
            return "Whenever you play a card with 'smog' in the name, gain 1 energy.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(this.Owner, new GuerillaMindsetStatusEffect(), 1);
        }
    }

    public class GuerillaMindsetStatusEffect : AbstractStatusEffect
    {
        public GuerillaMindsetStatusEffect()
        {
            Name = "Guerilla Mindset";
        }

        public override string Description => $"Whenever you play a smog card, gain {Stacks} energy";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool ownedByMe)
        {
            if (cardPlayed.NameContains("smog"))
            {
                state().energy += Stacks;
            }
        }
    }
}