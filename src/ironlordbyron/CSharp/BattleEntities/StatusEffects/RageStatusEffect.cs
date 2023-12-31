﻿namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.StatusEffects
{
    public class RageStatusEffect : AbstractStatusEffect
    {
        // Deal Stacks additional damage. Halves each turn.

        public RageStatusEffect()
        {
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("enrage");

            Name = "Rage";
        }

        public override string Description => $"Increase attack damage by {DisplayedStacks()}";

        public override int DamageDealtAddition()
        {
            return Stacks;
        }

        public override void OnTurnStart()
        {
            Action_HalveStacks();
        }
    }
}