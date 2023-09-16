using UnityEngine;
using System.Collections;

public class DyingStatusEffect : AbstractStatusEffect
{
    public DyingStatusEffect()
    {
        this.Name = "Dying";

        ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/death-skull");
        this.Stacks = 3;
    }

    public override string Description => $"This unit will die in {Stacks} turns.";

    public override void OnTurnEnd()
    {
        action().ApplyStatusEffect(OwnerUnit, new DyingStatusEffect(), -1);

        if (this.Stacks == 0)
        {
            action().KillUnit(OwnerUnit);
        }
    }
}
