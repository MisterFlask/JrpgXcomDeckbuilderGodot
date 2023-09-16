using UnityEngine;
using System.Collections;

public class SnappedStatusEffect : AbstractStatusEffect
{
    public SnappedStatusEffect()
    {
        Name = "Snapped";
        Stackable = true;
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("breaking-chain");
    }

    public override string Description => "Increases the cost of cards played by this character by [stacks]";
}
