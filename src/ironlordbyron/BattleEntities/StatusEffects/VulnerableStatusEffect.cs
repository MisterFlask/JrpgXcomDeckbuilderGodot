using System.Collections;

public class VulnerableStatusEffect : AbstractStatusEffect
{
    public VulnerableStatusEffect()
    {
        Name = "Vulnerable";
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("achilles-heel");
    }

    public override string Description => $"increases damage received by 50%";

    public override float DamageReceivedIncrementalMultiplier()
    {
        return 1.5f;
    }
}
