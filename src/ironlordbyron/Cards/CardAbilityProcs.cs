using Assets.CodeAssets.BattleEntities.StatusEffects;
using Assets.CodeAssets.Cards.BlackhandCards.Powers;
using Assets.CodeAssets.Cards.HammerCards.Common;
using System;
using System.Collections;
using System.Linq;

namespace Assets.CodeAssets.Cards
{
    /// <summary>
    /// This is just a set of helper methods for very specific card abilities-- leadership, sly, that sort of thing.
    /// </summary>
    public static class CardAbilityProcs
    {
        private static GameState state => GameState.Instance;
        private static ActionManager action => ActionManager.Instance;

        public static void Leadership(this AbstractCard card, Action thingToDo)
        {
            if (state.AllyUnitsInBattle.TrueForAll((ally) => ally == card.Owner || ally.CurrentLevel < card.Owner.CurrentLevel))
            {
                thingToDo();
            }
        }

        public static void GainEnergy(AbstractCard card, int amount = 1)
        {
            ActionManager.Instance.PushActionToBack("GainEnergy",() =>
            {
                GameState.Instance.energy += amount;
            });
        }

        public static void Refund(AbstractCard card, int amount = 1)
        {
            ActionManager.Instance.PushActionToBack("Refund",() =>
            {
                GameState.Instance.energy+=amount;
            });
        }

        public static void Sly(this AbstractCard card, Action thingToDo)
        {
            if (state.Deck.Hand.Count < 3)
            {
                thingToDo();
                BattleRules.TriggerProc(new SlyProc { TriggeringCardIfAny = card });

            }
        }

        // If ANY ally is Charged, decrement it by 1 and activate this effect.
        public static void Discharge(AbstractCard abstractCard, Action action)
        {
            var characterWithCharged =  GameState.Instance.AllyUnitsInBattle.First(item => item.HasStatusEffect<ChargedStatusEffect>());
            ActionManager.Instance.ApplyStatusEffect(characterWithCharged, new ChargedStatusEffect(), -1);
            action();
        }

        public static void Liquidate(this AbstractCard card, Action thingToDo)
        {
            var firstRareCard = state.Deck.Hand.FirstOrDefault(item => item.Rarity == Rarity.RARE && item != card);
            if (firstRareCard != null)
            {
                thingToDo(); 

                action.ExhaustCard(firstRareCard); 
                BattleRules.TriggerProc(new LiquidateProc { TriggeringCardIfAny = card });
            }
        }

        public static void ProcExert(this AbstractCard card)
        {
            var leftmostTwoCards = state.Deck
                .Hand
                .Where(item => item != card)
                .Take(2);

            foreach(var cardToDiscard in leftmostTwoCards)
            {
                action.DiscardCard(card);
            }
        }

        /// <summary>
        /// Nascent: When discarded, increase this card's defense and damage values by 2.
        /// </summary>
        public static void ProcNascent(this AbstractCard card)
        {
            if (card.BaseDamage != 0)
            {
                card.BaseDamage += 2;
            }

            if (card.BaseDefenseValue != 0)
            {
                card.BaseDefenseValue += 2;
            }
        }

        public static void Action_Exhaust(this AbstractCard card)
        {
            action.ExhaustCard(card);
        }

        public static bool IsVigil(this AbstractCard card)
        {
            var firstVigilCard = state.Deck.Hand.FirstOrDefault(item => item.CardTags.Contains(BattleCardTags.VIGIL));
            if (firstVigilCard == card)
            {
                return true;
            }
            return false;
        }

        public static void Brute(this AbstractCard card, Action thingToDo)
        {
            var cost3Card = state.Deck.Hand.FirstOrDefault(c => c.BaseEnergyCost() == 3);
            if (cost3Card != null)
            {
                thingToDo();
                BattleRules.TriggerProc(new BruteProc { TriggeringCardIfAny = card });

            }
        } 

        public static void Inferno(this AbstractCard card, Action thingToDo)
        {
            var burningEnemy = state.EnemyUnitsInBattle.Any(enemy => !enemy.IsDead && enemy.HasStatusEffect<BurningStatusEffect>());
            {
                thingToDo();
                BattleRules.TriggerProc(new InfernoProc { TriggeringCardIfAny = card });
            }
        }

        public static void Sacrifice(this AbstractCard card, Action thingToDo)
        {
            var sacrificableCard = state.Deck.Hand.FirstOrDefault(item => item != card);
            if (sacrificableCard != null)
            {
                sacrificableCard.Action_Exhaust();
                action.ApplyStress(sacrificableCard.Owner, 6);
                BattleRules.TriggerProc(new SacrificeProc { TriggeringCardIfAny = card });
                thingToDo();
            }
        }

        public static bool Ambush(this AbstractCard card, Action thingToDo)
        {
            if (GameState.Instance.BattleTurn <= 3)
            {
                thingToDo();
                return true;
            }
            return false;
        }

        public static void Patient(this AbstractCard card, Action thingToDo)
        {
            if (GameState.Instance.cardsPlayedThisTurn > 2)
            {
                thingToDo();
            }
        }

        internal static void ChangeMoney(int v)
        {
            GameState.Instance.Credits += v;
            if (GameState.Instance.Credits < 0)
            {
                GameState.Instance.Credits = 0;
            }
        }


        public static void Technocannibalize(AbstractCard cardUsed, Action actToPerform)
        {
            var cardsToExhaust = GameState.Instance.Deck.Hand.Where(item => item.WasCreated);
            int exhaustedCards = 0;
            foreach(var cardToExhaust in cardsToExhaust)
            {
                cardToExhaust.Action_Exhaust();
                exhaustedCards++;
            }
            for (int i = 0; i< exhaustedCards; i++)
            {
                actToPerform();
            }

        }

        public static void GainDataPoints(AbstractCard cardUsed, int numberDataPoints)
        {

        }
    }


    public class SacrificeProc: AbstractProc
    {

    }

    public class InfernoProc: AbstractProc
    {

    }

    public class BruteProc: AbstractProc
    {

    }

    public class LiquidateProc: AbstractProc
    {

    }

    public class SlyProc: AbstractProc
    {

    }

}