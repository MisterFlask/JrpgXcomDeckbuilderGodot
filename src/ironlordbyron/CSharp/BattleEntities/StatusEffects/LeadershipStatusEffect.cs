namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.StatusEffects
{
    public class LeadershipStatusEffect : AbstractStatusEffect
    {
        public LeadershipStatusEffect()
        {
            Name = "Leadership";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("public-speaker");
            Stackable = true;
            AllowedToGoNegative = true;
        }

        public override string Description => $"Whenever you apply defense, apply [Stacks] more.";

        public override int DefenseReceivedAddition()
        {
            return Stacks;
        }
    }
}