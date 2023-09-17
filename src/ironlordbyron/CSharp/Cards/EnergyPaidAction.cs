using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards
{
    public abstract class EnergyPaidAction
    {
        public abstract void OnCardPlayed(AbstractCard card);
    }

    public class EnergyPaidInformation
    {
        public List<EnergyPaidAction> ActionsToTake { get; set; } = new List<EnergyPaidAction>();

        public int EnergyCost { get; set; }
    }
}