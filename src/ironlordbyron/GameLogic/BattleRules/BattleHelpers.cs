using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards
{
    public static class BattleHelpers
    {
        private static GameState state => GameState.Instance;

        public static IEnumerable<AbstractCard> CardsInHandInOrder()
        {
            return state.Deck.Hand;
        }

        public static bool DoesAnyEnemyHave<T>() where T: AbstractStatusEffect
        {
            var enemyHasStatusEffect = state.EnemyUnitsInBattle
                .SelectMany(item => item.StatusEffects)
                    .Any(item => item.GetType() == typeof(T));

            return enemyHasStatusEffect;
        }

        public static bool IsLeader(this AbstractBattleUnit unit)
        {
            var allies = state.AllyUnitsInBattle.Where(item => item != unit);
            return allies.All(item => item.CurrentLevel < unit.CurrentLevel);
        }

        public static IEnumerable<AbstractBattleUnit> AllUnitsInRoster => state.PersistentCharacterRoster;

        public static IEnumerable<AbstractCard> NonExhaustedCardsInDeck()
        {
            return state.Deck.
                DiscardPile
                .Concat(state.Deck.DrawPile)
                .Concat(state.Deck.Hand);
        }

        public static AbstractCard GetActiveVigilCard()
        {
            return LeftmostCardInHandThat(item => item.CardTags.Contains(BattleCardTags.VIGIL));
        }

        public static AbstractCard LeftmostCardInHandThat(Predicate<AbstractCard> pred)
        {
            foreach(var card in CardsInHandInOrder())
            {
                if (pred(card))
                {
                    return card;
                }
            }
            return null;
        }
        public static AbstractCard RandomCardInHandThat(Predicate<AbstractCard> pred)
        {
            foreach (var card in CardsInHandInOrder().Shuffle())
            {
                if (pred(card))
                {
                    return card;
                }
            }
            return null;
        }

    }
}