using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.StatusEffects
{
    public class TemporaryStrengthStatusEffect : AbstractStatusEffect
    {
        public TemporaryStrengthStatusEffect()
        {
            this.Name = "Temporary Strength";
            this.AllowedToGoNegative = true; 
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("weight-lifting-up");

        }

        public override int DamageDealtAddition()
        {
            return Stacks;
        }

        public override void OnTurnEnd()
        {
            Stacks = 0;
        }

        public override string Description => $"Deal {DisplayedStacks()} additional damage with attacks.  Lose it at end of turn.";
    }
}