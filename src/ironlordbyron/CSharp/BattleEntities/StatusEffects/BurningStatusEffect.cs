public class BurningStatusEffect : AbstractStatusEffect
{
    public override string Description => $"Target takes damage per turn equal to the number of stacks of burning, then decreases Stacks by 2.  If the target has Flammable when Burning is applied, it consumes all stacks of Flammable and applies that much Burning.";

    public BurningStatusEffect()
    {
        Name = "Burning";
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("burning-forest");
    }

    public override void OnTurnEnd()
    {
        ActionManager.Instance.DamageUnitNonAttack(OwnerUnit, null, Stacks);
        Stacks -= 2;
    }

    public override void OnApplicationOrIncrease()
    {
        var stacksOfFlammable = OwnerUnit.GetStatusEffect<FumesStatusEffect>();
        if (stacksOfFlammable != null)
        {
            var stacks = stacksOfFlammable.Stacks;
            ActionManager.Instance.RemoveStatusEffect<FumesStatusEffect>(OwnerUnit);
            ActionManager.Instance.ApplyStatusEffect(OwnerUnit, new BurningStatusEffect(), stacks);
        }
    }
}
