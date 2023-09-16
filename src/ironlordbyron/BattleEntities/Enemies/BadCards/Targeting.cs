using Assets.CodeAssets.Cards;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.BadCards
{
    public class Targeting : AbstractCard
    {
        public Targeting()
        {
            Name = "Targeting Omen";
            StaticBaseEnergyCost = 1;
            
        }


        // Drawn: if there are three of these in hand at once, take 30 damage and exhaust all of them.  Retain.  
        // Costs 2 to play and exhaust.
        public override string DescriptionInner()
        {
            return $"Played: Exhaust.  Drawn: If there are three of these in hand at once, take 5 damage and exhaust.  Retain.";
        }

        public override bool ShouldRetainCardInHandAtEndOfTurn()
        {
            return true;
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_Exhaust();
        }

        public override void OnDrawInner()
        {
            var cardsOfThisTypeInHand = state().Deck.Hand.Where(item => item.GetType() == typeof(Targeting));
            if (cardsOfThisTypeInHand.Count() >= 3)
            {
                action().DamageUnitNonAttack(Owner, null, 5);
                Action_Exhaust();
            }
        }
    }
}