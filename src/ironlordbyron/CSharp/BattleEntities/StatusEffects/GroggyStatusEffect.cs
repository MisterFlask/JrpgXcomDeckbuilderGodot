namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.StatusEffects
{
    public class GroggyStatusEffect : AbstractStatusEffect
    {
        public GroggyStatusEffect()
        {
            Name = "Groggy";
        }

        public override string Description => $"Apply {DisplayedStacks()} less block with cards, decreasing by 1 each turn.";

        public override int DefenseDealtAddition()
        {
            return -1 * Stacks;
        }

        public override void OnTurnEnd()
        {
            Stacks--;
        }
    }
}