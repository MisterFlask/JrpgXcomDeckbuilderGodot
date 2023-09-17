using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public class BattleDeck
{
    public IEnumerable<AbstractCard> TotalDeckList => CollectionUtils.Aggregate(DrawPile, DiscardPile, Hand);

    public List<AbstractCard> ExhaustPile { get; set; } = new List<AbstractCard>();
    public List<AbstractCard> DrawPile { get; set; } = new List<AbstractCard>();

    public List<AbstractCard> DiscardPile { get; set; } = new List<AbstractCard>();

    public List<AbstractCard> Hand { get; set; } = new List<AbstractCard>();

    public List<AbstractCard> DrawHandAndDiscardPiles => DrawPile.Concat(Hand).Concat(DiscardPile).ToList();

    public CardPosition GetCardPosition(string cardId)
    {
        if (Hand.Where(item => item.Id == cardId).Any())
        {
            return CardPosition.HAND;
        }
        if (DrawPile.Where(item => item.Id == cardId).Any())
        {
            return CardPosition.DRAW;
        }
        if (DiscardPile.Where(item => item.Id == cardId).Any())
        {
            return CardPosition.DISCARD;
        }
        if (ExhaustPile.Where(item => item.Id == cardId).Any())
        {
            return CardPosition.EXPENDED;
        }
        Log.Info($"Could not find card {cardId}; returning Purged");
        return CardPosition.EXPENDED;
    }

    public CardPosition PurgeCardFromDeck(string id)
    {
        if (!TotalDeckList.Where(i => i.Id == id).Any())
        {
            throw new Exception("Could not find card with ID of " + id);
        }
        var card = TotalDeckList.Where(i => i.Id == id).Single();
        if (DrawPile.Contains(card))
        {
            DrawPile.Remove(card);
            return CardPosition.DRAW;

        }
        if (DiscardPile.Contains(card))
        {
            DiscardPile.Remove(card);
            return CardPosition.DISCARD;
        }
        if (Hand.Contains(card))
        {
            Hand.Remove(card);
            return CardPosition.HAND;
        }
        throw new Exception("This should be impossible");
    }

    public AbstractCard GetRandomMatchingCard(Func<AbstractCard, bool> pred)
    {
        var matchesPred = this.TotalDeckList.Where(pred);
        if (matchesPred.IsEmpty())
        {
            return null;
        }
        return matchesPred.PickRandom();
    }

    public void DiscardCardFromHand(AbstractCard card)
    {
        Hand.Remove(card);
        DiscardPile.Add(card);
    }

    private List<AbstractCard> GetPileForPosition(CardPosition position)
    {
        if (position == CardPosition.DISCARD)
        {
            return DiscardPile;
        }
        if (position == CardPosition.DRAW)
        {
            return DrawPile;
        }
        if (position == CardPosition.HAND)
        {
            return Hand;
        }
        if (position == CardPosition.EXPENDED)
        {
            return ExhaustPile;
        }
        throw new Exception($"Don't know about position {position}");
    }

    public void MoveCardToPile(AbstractCard card, CardPosition position, bool newCardsAllowed = false)
    {
        var fromPosition = GetCardPosition(card.Id);
        var fromPile = GetPileForPosition(fromPosition);
        if (!newCardsAllowed && !TotalDeckList.Contains(card))
        {
            Log.Info("Card did not exist prior to pile move");
        }
        var toPile = GetPileForPosition(position);
        if (fromPile == null)
        {
            throw new Exception("Could not find card with name " + card.Name);
        }
        fromPile.RemoveAll((item) => item.Id == card.Id);
        toPile.Add(card);
    }

    public void AddNewCardToDiscardPile(AbstractCard card)
    {
        if (TotalDeckList.Any(item => item.Id == card.Id))
        {
            throw new Exception("Attempted to add duplicate card to deck: " + card.Name);
        }
        DiscardPile.Add(card);
    }

    public void ReshuffleDeck()
    {
        DrawPile.AddRange(DiscardPile);
        DiscardPile.Clear();
        DrawPile = DrawPile.Shuffle().ToList();
    }

    public List<AbstractCard> DrawNextNCards(int n)
    {
        var cardsSoFar = new List<AbstractCard>();
        int cardsToStart = TotalDeckList.Count();
        try
        {

            for (int i = 0; i < n; i++)
            {
                if (DrawPile.IsEmpty())
                {
                    ReshuffleDeck();
                    if (DrawPile.IsEmpty())
                    {
                        GD.Print("Deck is empty, discard is empty, yet still we attempt to draw");
                        this.Hand.AddRange(cardsSoFar);

                        return cardsSoFar;
                    }
                }
                cardsSoFar.Add(DrawPile.PopFirstElement());
            }

            this.Hand.AddRange(cardsSoFar);
        }
        finally
        {
            var cardsAfterShuffle = TotalDeckList.Count();

            if (cardsToStart != cardsAfterShuffle)
            {
                throw new Exception("Validation failure: after shuffle had " + cardsAfterShuffle + " and cards to start were " + cardsToStart);
            }
        }
        return cardsSoFar;
    }

}
public enum CardPosition
{
    HAND,
    DRAW,
    DISCARD,
    EXPENDED
}