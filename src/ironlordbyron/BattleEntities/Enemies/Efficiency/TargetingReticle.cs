using Assets.CodeAssets.Cards;
using System.Collections;
using System.Linq;

namespace Assets.CodeAssets.BattleEntities.Enemies.Efficiency
{

    /// <summary>
    ///  Retain.  Exhaust.  At the end of turn, if there are three of these in hand, get dealt 20 damage.
    /// </summary>
    public class TargetingReticle : AbstractCard
    {
        // Set to "true" if we don't want any more activations this turn.
        private static bool ActiveFlag = false;

        public TargetingReticle()
        {
            Name = "Efficiency Targeting Reticle";
            this.ProtoSprite = ProtoGameSprite.StatusCardIcon("targeting-reticle");
            CardType = CardType.ConditionCard;

        }

        public override string DescriptionInner()
        {
            return "Retain.  Exhaust.  At the end of turn, if there are two of these in hand, owner gets dealt 20 damage.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_Exhaust();
        }

        public override bool ShouldRetainCardInHandAtEndOfTurn()
        {
            return true;
        }

        public override void InHandAtEndOfTurnAction()
        {
            if (state().Deck.Hand.Where(item => item is TargetingReticle).Count() >= 2)
            {
                action().DamageUnitNonAttack(Owner, null, 20);
            }
        }
    }
}