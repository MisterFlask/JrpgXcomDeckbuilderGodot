using System.Collections;

namespace Assets.CodeAssets.BattleEntities.StatusEffects
{
    public class LeadershipStatusEffect : AbstractStatusEffect
    {
        public LeadershipStatusEffect()
        {
            this.Name = "Leadership";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("public-speaker");
            this.Stackable = true;
            this.AllowedToGoNegative = true;
        }

        public override string Description => $"Whenever you apply defense, apply [Stacks] more.";

        public override int DefenseReceivedAddition()
        {
            return Stacks;
        }
    }
}