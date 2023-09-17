using System.Collections;

public class RetaliateStatusEffect : AbstractStatusEffect
{
    public RetaliateStatusEffect()
    {
        Name = "Retaliate";
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("armor-punch");
    }

    public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
    {
        if (Stacks <= 0) return;
        var damageToReturn = BattleRules.GetAnticipatedDamageToUnit(OwnerUnit, unitStriking, 5, true, null);
        var enhancerStacks = unitStriking.GetStatusEffect<GentleDiscouragementStatusEffect>()?.Stacks ?? 0;
        action().DamageUnitNonAttack(unitStriking, null, damageToReturn + enhancerStacks);
        Stacks--;
    }

    private string GetDisplayedRetaliateDamage()
    {
        if (OwnerUnit == null) return "";
        var damageToReturn = BattleRules.GetAnticipatedDamageToUnit(OwnerUnit, null, 5, true, null);
        return $"[Will deal {damageToReturn} per hit before enemy modifiers]";
    }

    public override string Description => "When this character is attacked, tick down stacks by 1 and deal a flat 5 damage to attackers." +
        "Scales with all damage modifiers.  " + GetDisplayedRetaliateDamage();
}


public class GentleDiscouragementStatusEffect: AbstractStatusEffect
{
    public GentleDiscouragementStatusEffect()
    {
        Name = "Gentle Discouragement";
    }

    public override string Description => $"Retaliate deals {Stacks} more damage";

}