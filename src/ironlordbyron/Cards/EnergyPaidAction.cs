using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.Cards
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