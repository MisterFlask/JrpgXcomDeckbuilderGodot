using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards.Rare
{
    public class Professional : AbstractCard
    {
        // Apply 1 block and 1 Retaliate.  If you have a card with Gilded in hand, gain 1 more Retaliate.
        // EACH time you finish a mission with this card in your deck, it applies an additional 4 block and 1 retaliate.

        public Professional()
        {
            SetCommonCardAttributes("Professional", Rarity.RARE, TargetType.ALLY, CardType.SkillCard, 1);
            MagicNumber = 1;
            BaseDefenseValue = 1;
            ProtoSprite = ProtoGameSprite.HammerIcon("pikeman");

        }

        public override string DescriptionInner()
        {
            return $"Apply {BaseDefenseValue} block and gain {MagicNumber} Retaliate.  If you have a card with Gilded in hand, gain 1 more Retaliate.  "
                + $"EACH time you finish a mission with this card in your deck, it applies an additional 4 block and 1 retaliate.";
        }

        public override void IsNotExhaustedInDeckAtEndOfBattle()
        {
            this.CorrespondingPermanentCard().MagicNumber++;
            this.CorrespondingPermanentCard().BaseDefenseValue += 4;
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyDefense(target, Owner, BaseDefenseValue);
            var retaliateApplied = MagicNumber;
            if (state().Deck.Hand.Any(item => item.HasSticker<GildedCardSticker>()))
            {
                retaliateApplied += 1;
            }
            action().ApplyStatusEffect(target, new RetaliateStatusEffect(), retaliateApplied);
        }
    }
}