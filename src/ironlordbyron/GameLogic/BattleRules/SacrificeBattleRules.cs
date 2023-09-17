using System.Collections;
using System.Linq;

namespace Assets.CodeAssets.GameLogic.BattleRules
{
    public class SacrificeBattleRules
    {
        public static bool CanPlay(AbstractCard card)
        {
            var otherCardsInHand = GameState.Instance.Deck.Hand.Where(item => item.Id != card.Id);
            if (otherCardsInHand.IsEmpty())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void RunSacrificeRules(AbstractCard card)
        {
            var otherCardsInHand = GameState.Instance.Deck.Hand.Where(item => item.Id != card.Id);
            if (otherCardsInHand.IsEmpty())
            {
                return;
            }
            var cardToExhaust = otherCardsInHand.PickRandom();
            ActionManager.Instance.ExhaustCard(cardToExhaust);
            ActionManager.Instance.ApplyStatusEffect(cardToExhaust.Owner, new StressStatusEffect(), 8);
        }
    }
}