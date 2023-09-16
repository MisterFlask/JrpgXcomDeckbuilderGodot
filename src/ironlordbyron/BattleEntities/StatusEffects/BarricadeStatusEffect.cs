using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.StatusEffects
{
    public class BarricadeStatusEffect : AbstractStatusEffect
    {
        // Decreases by half at the beginning of each turn.  Decrease incoming damage by {stacks}.

        public BarricadeStatusEffect()
        {
            Name = "Barricade";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("barricade");

        }

        public override string Description => $"Decrease received damage by {DisplayedStacks()}.  Decreases by half each turn.";

        public override int DamageReceivedAddition()
        {
            return -1 * Stacks;
        }

        public override void OnTurnStart()
        {
            Action_HalveStacks();
        }
    }
}