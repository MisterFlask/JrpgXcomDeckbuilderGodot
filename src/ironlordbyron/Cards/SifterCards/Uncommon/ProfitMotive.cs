using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.SifterCards.Uncommon
{
    public class ProfitMotive : AbstractCard
    {
        public ProfitMotive()
        {
            SetCommonCardAttributes("Profit Motive", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1);

            this.ProtoSprite =
                ProtoGameSprite.ArchonIcon("coins");
        }

        // Whenever you trigger a Lethal or Bounty effect, draw a card and gain 5 credits.
        public override string DescriptionInner()
        {
            return $"Whenever you trigger a Lethal or Bounty effect, draw a card and gain 5 credits.";//bounty is implicit in lethal
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToOwner(new ProfitMotiveStatusEffect(), 1);
        }
    }


    public class ProfitMotiveStatusEffect : AbstractStatusEffect
    {
        public ProfitMotiveStatusEffect()
        {
            Name = "Profit Motive";
        }

        public override string Description => $"Whenever you trigger a Lethal or Bounty effect, draw {DisplayedStacks()} cards and gain five times that many credits.";

        public override void ProcessProc(AbstractProc proc)
        {
            if (proc is LethalTriggerProc)
            {
                action().DrawCards(1 * Stacks);
                CardAbilityProcs.ChangeMoney(5 * Stacks);
            }
        }
    }
}