using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Effects
{
    public class AscendanceStatusEffect : AbstractStatusEffect
    {
        public static string NAME = "Ascendance";

        public AscendanceStatusEffect()
        {
            this.Name = "Ascendance";
        }

        public override string Description => $"This character gains {DisplayedStacks()} Power each turn.";

        public override int DamageDealtAddition()
        {
            return 2;
        }

        public override void OnTurnEnd()
        {
            action().ApplyStatusEffect(OwnerUnit, new StrengthStatusEffect());
        }
    }
}