

public class TemporaryThorns : AbstractStatusEffect
{
    public TemporaryThorns()
    {
        Name = "Temporary Thorns";
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("spiked-tentacle");

    }

    public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
    {
        action().DamageUnitNonAttack(unitStriking, null, Stacks);
    }

    public override string Description => "Deals damage to attackers equal to number of stacks.  Goes away at start of next turn.";

    public override void OnTurnEnd()
    {
        action().RemoveStatusEffect<TemporaryThorns>(OwnerUnit);
    }
}
