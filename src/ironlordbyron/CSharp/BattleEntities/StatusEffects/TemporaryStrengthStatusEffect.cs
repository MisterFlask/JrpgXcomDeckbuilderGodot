namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.StatusEffects
{
    public class TemporaryStrengthStatusEffect : AbstractStatusEffect
    {
        public TemporaryStrengthStatusEffect()
        {
            Name = "Temporary Strength";
            AllowedToGoNegative = true;
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("weight-lifting-up");

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