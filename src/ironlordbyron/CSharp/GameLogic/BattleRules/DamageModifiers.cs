using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards;
using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects;
using System.Linq;

public abstract class DamageModifier
{
    public GameState state() => GameState.Instance;
    public ActionManager action() => ActionManager.Instance;


    public bool IgnoresDefense { get; set; } = false;

    public bool IgnoresRetaliation { get; set; } = false;

    /// <summary>
    /// If true, this will get incorporated in damage calcs when just looking at the card.
    /// </summary>
    public bool TargetInvariant { get; set; } = false;

    public string CardDescriptionAddendum { get; set; } = "Sample";

    public string TooltipDescription { get; set; } = "Tooltip Text";

    /// <summary>
    /// Return "True" if this procs the slay trigger.
    /// </summary>
    public virtual bool SlayInner(AbstractCard damageSource, AbstractBattleUnit target)
    {
        return false;
    }

    public void Slay(AbstractCard damageSource, AbstractBattleUnit target)
    {
        var slew = SlayInner(damageSource, target);
        if (slew)
        {
            BattleRules.TriggerProc(new LethalTriggerProc());
        }
    }

    public virtual string GetDescriptionAddendum()
    {
        return CardDescriptionAddendum;
    }

    public virtual void OnStrike(AbstractCard damageSource, AbstractBattleUnit target, int totalDamageAfterModifiers)
    {
    }
    public virtual void OnPlay(AbstractCard damageSource, AbstractBattleUnit target)
    {
    }

    public virtual int GetIncrementalDamageAddition(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
    {
        // note: can get owner from damageSource if necessary
        return 0;
    }

    /// <summary>
    ///  we add all multipliers together before actually using them.
    /// </summary>
    public virtual float GetIncrementalDamageMultiplier(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
    {
        // note: can get owner from damageSource if necessary
        return 0;
    }

    public virtual int GetIncrementalBlockAddition(int currentBaseBlock, AbstractCard blockSource, AbstractBattleUnit target)
    {
        return 0;
    }
}

public class LethalTriggerProc : AbstractProc
{

}

public class BusterDamageModifier : DamageModifier
{
    public BusterDamageModifier()
    {
        this.CardDescriptionAddendum = "Buster";
        this.TooltipDescription = "Deal +50% damage to targets with Block.";
    }

    public override float GetIncrementalDamageMultiplier(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
    {
        if (target.CurrentBlock > 0)
        {
            return .5f;
        }
        return 0;
    }
}


public class PrecisionDamageModifier : DamageModifier
{

    public PrecisionDamageModifier()
    {
        this.CardDescriptionAddendum = "Precision";
        this.TooltipDescription = "Ignores Block.  If attacking a Marked target, gain +50% damage.";
        IgnoresDefense = true;
    }

    public override float GetIncrementalDamageMultiplier(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
    {
        if (target.HasStatusEffect<MarkedStatusEffect>())
        {
            return .5f;
        }
        return 0;
    }
}

public class SlayerDamageModifier : DamageModifier
{
    public SlayerDamageModifier()
    {
        this.CardDescriptionAddendum = "Anti-Titan";
        this.TooltipDescription = "Deals +50% damage to targets with >300 maximum hit points.";
    }
    public override float GetIncrementalDamageMultiplier(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
    {
        if (target.MaxHp > 300)
        {
            return .5f;
        }
        return 0;
    }
}
public class SweeperDamageModifier : DamageModifier
{
    public SweeperDamageModifier()
    {
        this.CardDescriptionAddendum = "Sweeper";
        this.TooltipDescription = "Attack also deals 25% pre-modifier damage to up to 2 other random targets.";
    }

    public override void OnPlay(AbstractCard damageSource, AbstractBattleUnit target)
    {
        var otherPossibleTargets = GameState.Instance.EnemyUnitsInBattle
            .Where(item => item != target)
            .Where(item => item.IsTargetable())
            .Shuffle()
            .TakeUpTo(2)
            .ToList();
        ActionManager.Instance.AttackUnitForDamage(target, damageSource.Owner, damageSource.BaseDamage / 4, damageSource);
    }
}

public class StrengthScalingDamageModifier : DamageModifier
{
    private int additionalScaling = 0;
    public StrengthScalingDamageModifier(int additionalStrengthScaling)
    {
        this.TooltipDescription = $"This attack gains {1 + additionalStrengthScaling}x damage from strength";
        this.additionalScaling = additionalStrengthScaling;
        this.TargetInvariant = true;
    }

    public override string GetDescriptionAddendum()
    {
        return "Additional Strength Scaling : " + additionalScaling;
    }

    public override int GetIncrementalDamageAddition(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
    {
        return additionalScaling * damageSource?.Owner?.GetStatusEffect<StrengthStatusEffect>()?.Stacks ?? 0;
    }

}


public class BountyDamageModifier : DamageModifier
{
    public static BountyDamageModifier Create()
    {
        return new BountyDamageModifier();
    }
    public static BountyDamageModifier GetBountyDamageModifier()
    {
        return new BountyDamageModifier();
    }

    public BountyDamageModifier()
    {
        CardDescriptionAddendum = "Bounty";
        TooltipDescription = "If this unit kills a:  Boss -> 20 credits, Elite -> 10 credits, other non-minion -> 5 credits.";
    }

    public override bool SlayInner(AbstractCard damageSource, AbstractBattleUnit target)
    {
        if (target.IsBoss)
        {
            CardAbilityProcs.ChangeMoney(20);
        }
        if (target.IsElite)
        {
            CardAbilityProcs.ChangeMoney(10);
        }
        else
        {
            //todo: Minion exclusion
            CardAbilityProcs.ChangeMoney(5);
        }
        return true;
    }
}

// If this targets the owner of the card, it applies +4 defense.
public class SelfishDefenseModifier : DamageModifier
{
    public override int GetIncrementalBlockAddition(int currentBaseBlock, AbstractCard blockSource, AbstractBattleUnit target)
    {
        if (blockSource.Owner == target)
        {
            return 4;
        }
        return 0;
    }
}

public class GainDataPointsOnSlayDamageModifier : DamageModifier
{

    public GainDataPointsOnSlayDamageModifier()
    {
    }

    public int DataPointsToAcquire { get; set; } = 1;
    public override bool SlayInner(AbstractCard damageSource, AbstractBattleUnit target)
    {
        CardAbilityProcs.GainDataPoints(damageSource, DataPointsToAcquire);
        return true;
    }

    public override string GetDescriptionAddendum()
    {
        return $"Slay: Gain {DataPointsToAcquire} data points.";
    }
}

/// <summary>
/// If I have less Might [todo: Might is just the net damage increase] than the target, apply +50% defense.
/// </summary>
public class ProtectorDefenseModifier : DamageModifier
{
    //todo
}