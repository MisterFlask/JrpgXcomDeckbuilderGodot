using System.Collections;

public class WeakenedStatusEffect : AbstractStatusEffect
{
    public WeakenedStatusEffect()
    {
        this.Name = "Weak";
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("broken-axe");
    }

    public override string Description => "Reduces damage by 1/3.  On stack is removed per turn.";

    public override float DamageDealtIncrementalMultiplier()
    {
        return -.333f;
    }
}
