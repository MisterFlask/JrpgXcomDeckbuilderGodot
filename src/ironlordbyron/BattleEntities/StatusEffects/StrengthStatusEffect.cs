using System.Collections;

public class StrengthStatusEffect : AbstractStatusEffect
{
    public StrengthStatusEffect()
    {
        this.Name = "Power";
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("weight-lifting-up");
        this.Stackable = true;
        this.AllowedToGoNegative = true;
    }

    public override string Description => $"Increases damage by 1 per stack.";

    public override int DamageDealtAddition()
    {
        return Stacks;
    }
}
