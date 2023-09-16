using System;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Effects
{
    public class StressDefenseStatusEffect : AbstractStatusEffect
    {

        public StressDefenseStatusEffect()
        {
            Name = "Stress Defense";
        }

        public override string Description => $"Absorbs then next {DisplayedStacks()} Stress this character would receive.";

        public override int OverrideStatusEffectApplicationToOwner(
            AbstractStatusEffect statusEffectApplied,
            int numStacksApplied)
        {
            if (numStacksApplied < 0)
            {
                return numStacksApplied;
            }

            if (this.Stacks <= 0) // if we're removing stress, we're not modifying that
            {
                this.Stacks = 0;
                return numStacksApplied;
            }

            if (statusEffectApplied.GetType() == typeof(StressStatusEffect))
            {
                var stacksToRemove = 0;
                if (numStacksApplied > this.Stacks)
                {
                    stacksToRemove = this.Stacks;
                    this.Stacks = 0;
                    return numStacksApplied - stacksToRemove;
                }
                if (Stacks >= numStacksApplied)
                {
                    Stacks -= numStacksApplied;
                    return 0;
                }
            }
            return numStacksApplied;
        }
    }
}