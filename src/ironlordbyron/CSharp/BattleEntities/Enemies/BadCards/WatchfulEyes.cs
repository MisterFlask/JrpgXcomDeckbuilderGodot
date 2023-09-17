using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.BadCards
{
    public class WatchfulEyes : AbstractCard
    {
        public WatchfulEyes()
        {
            Name = "Watchful Eyes";
            StaticBaseEnergyCost = 1;
        }


        // retained: Increase doom counter by 1.  Exhaust for 1.
        public override string DescriptionInner()
        {
            return "Retain.  Retained: Increase doom counter by 1.  Exhaust for 1.";
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
            action().IncrementDoomCounter(1);
        }
    }
}