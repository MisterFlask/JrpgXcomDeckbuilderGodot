using UnityEngine;
using System.Collections;

public class FumesStatusEffect : AbstractStatusEffect
{
    public FumesStatusEffect()
    {
        Name = "Fumes";
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("matchbox");
    }
    public override string Description => "Target deals a flat -2 damage. Ticks down by half its stacks per turn.  " +
        "If Fuel is applied, removes all Flammable and adds that much Burning.";

    public override void OnTurnEnd()
    {
        action().ApplyStatusEffect(this.OwnerUnit, new FumesStatusEffect(), -1 * Stacks / 2);
    }

    public override int DefenseDealtAddition()
    {
        return -2;
    }
}
