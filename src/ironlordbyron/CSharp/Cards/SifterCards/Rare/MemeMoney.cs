using System.Linq;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.SifterCards.Rare
{
    public class MemeMoney : AbstractCard
    {
        // Deal damage equal to the total Hoard value of your deck.  Exhaust.  Cost 2.

        public MemeMoney()
        {
            SetCommonCardAttributes("Stupid Meme Money", Rarity.RARE, TargetType.ENEMY, CardType.AttackCard, 0);
            Stickers.Add(new GildedCardSticker(4));
            ProtoSprite = ProtoGameSprite.CogIcon("shiny-purses");

        }

        public override string DescriptionInner()
        {
            return $"Deal damage equal to the total number of Gilded cards in your deck. [This is {GetHoardValueOfDeck()}]";
        }

        private int GetHoardValueOfDeck()
        {
            var deck = state().Deck;
            return deck.DiscardPile.Concat(deck.DrawPile).Concat(deck.Hand).Select(item => item.HasSticker<GildedCardSticker>()).Count();
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            ActionManager.Instance.AttackUnitForDamage(target, Owner, GetHoardValueOfDeck(), this);
        }
    }
}