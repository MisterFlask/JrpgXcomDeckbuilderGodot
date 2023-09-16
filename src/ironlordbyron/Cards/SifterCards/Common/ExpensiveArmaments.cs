using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards.SifterCards.Common
{
    public class ExpensiveArmaments : AbstractCard
    {
        // A random card in your hand gains Gilded 2.  All your attacks with Hoard deal 4 more damage.  Exhaust.

        public ExpensiveArmaments()
        {
            SetCommonCardAttributes("Expensive Armaments", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1);

            this.ProtoSprite =
                ProtoGameSprite.ArchonIcon("relic-blade");
        }

        public override string DescriptionInner()
        {
            return $"A random card in your hand gains Gilded 2.  ALL attacks in your deck with Gilded deal 4 more damage.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            var randomAttackCardInHand = CardsInHand.Shuffle().FirstOrDefault(item => item != this && item.CardType == CardType.AttackCard);
            if (randomAttackCardInHand != null)
            {
                randomAttackCardInHand.AddSticker(new GildedCardSticker(2));
            }


            foreach (var attack in state().Deck.TotalDeckList.Where(item => item.CardType == CardType.AttackCard && item.HasSticker<GildedCardSticker>()))
            {

            }
        }
    }
}