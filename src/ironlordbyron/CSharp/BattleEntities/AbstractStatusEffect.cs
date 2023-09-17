using GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic;
using System.Collections.Generic;

public abstract class AbstractStatusEffect : MagicWord
{
    public BattleUnitAttributePrefab CorrespondingPrefab { get; set; }

    #region convenience functions
    public ActionManager action()
    {
        return ServiceLocator.GetActionManager();
    }

    public List<AbstractBattleUnit> enemies()
    {
        return ServiceLocator.GameState().EnemyUnitsInBattle;
    }
    public List<AbstractBattleUnit> allies()
    {
        return ServiceLocator.GameState().AllyUnitsInBattle;
    }

    public GameState state()
    {
        return ServiceLocator.GameState();
    }
    #endregion
    public StatusLocalityType StatusLocalityType = StatusLocalityType.UNIT;

    //This is used for keeping track of stuff like "how many cards have been played"
    public int? InternalCounter = null;
    public StatusPolarityType StatusPolarityType { get; set; } = StatusPolarityType.NEITHER;
    public AbstractCard ReferencedCard { get; set; }
    public override string MagicWordTitle => this.Name;
    public override string MagicWordDescription => this.Description;

    public bool AllowedToGoNegative = false;
    public string Name { get; set; }
    public abstract string Description { get; }
    public AbstractBattleUnit OwnerUnit { get; set; }
    public bool Stackable { get; set; } = true;
    public int Stacks { get; set; } = 1;

    // Secondary indicator, for use in (e.g.) counting during a turn
    public int SecondaryStacks { get; set; }

    public bool IsExample { get; set; }

    public string DisplayedStacks()
    {
        if (IsExample)
        {
            return "[stacks]";
        }
        else
        {
            return $"{Stacks}";
        }
    }

    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon();

    public AbstractStatusEffect()
    {
        Name = GetType().Name;
    }

    public virtual void OnRemoval()
    {

    }

    /// <summary>
    ///  This should happen mostly on damage effects on the enemy.
    /// </summary>
    public virtual void OnTurnEnd()
    {

    }

    /// <summary>
    ///  This should happen mostly on debuff effects on the enemy.
    /// </summary>
    public virtual void OnTurnStart()
    {

    }

    public virtual void OnDeath(AbstractBattleUnit unitThatKilledMe, AbstractCard cardUsedIfAny)
    {

    }

    public virtual void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
    {

    }

    public virtual void OnStriking(AbstractBattleUnit unitStruck, AbstractCard cardUsedIfAny, int damageAfterBlockingAndModifiers)
    {

    }

    public virtual int DamageDealtAddition()
    {
        return 0;
    }

    public virtual int DamageReceivedAddition()
    {
        return 0;
    }

    public virtual float DamageDealtIncrementalMultiplier()
    {
        return 0;
    }

    public virtual float DamageReceivedIncrementalMultiplier()
    {
        return 0;
    }


    public virtual int DefenseDealtAddition()
    {
        return 0;
    }

    public virtual int DefenseReceivedAddition()
    {
        return 0;
    }

    public virtual float DefenseDealtIncrementalMultiplier()
    {
        return 0;
    }

    public virtual float DefenseReceivedIncrementalMultiplier()
    {
        return 0;
    }

    public void AssignOwner(AbstractBattleUnit unit)
    {
        if (OwnerUnit != null)
        {
            throw new System.Exception("Cannot reassign status effects.");
        }
        OwnerUnit = unit;
    }

    public AbstractStatusEffect CloneStatusEffect()
    {
        return (AbstractStatusEffect)this.MemberwiseClone();
    }

    public virtual void OnDealingUnblockedDamage(AbstractBattleUnit victim)
    {

    }

    public virtual void OnApplicationOrIncrease()
    {

    }

    // Returns the number of stacks that are to be applied, after processing.
    public virtual int OverrideStatusEffectApplicationToOwner(AbstractStatusEffect statusEffectApplied, int stacksAppliedOrDecremented)
    {
        return stacksAppliedOrDecremented;
    }


    public virtual void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool cardIsOwnedByMe)
    {

    }
    public virtual void OnAnyCardDrawn(AbstractCard cardDrawn, bool cardIsOwnedByMe)
    {

    }


    /// <summary>
    /// Note that this BOTH modifies the damage AND does anything relating to the damage modification (such as decreasing stacks of the mitigating attribute.)
    /// This is expected to mitigate damage AFTER block is consumed.  PRE-BLOCK damage is not impacted by this method.
    /// </summary>
    /// <param name="damageBlob"></param>
    public virtual void ModifyPostBlockDamageTaken(DamageBlob damageBlob)
    {

    }

    public virtual void ProcessProc(AbstractProc proc)
    {

    }

    public virtual int CardCostModifier(AbstractCard card, bool ownedByMe)
    {
        return 0;
    }

    public void HalveStacks()
    {
        if (Stacks % 2 == 1)
        {
            Stacks -= 1;
        }
        Stacks = Stacks / 2;
    }

    public void DecrementStacks()
    {
        Stacks--;
    }
    public void Action_HalveStacks()
    {
        ActionManager.Instance.PushActionToBack("HalveStacks", () =>
        {
            HalveStacks();
        });
    }


    public void Action_DecrementStacks()
    {
        ActionManager.Instance.PushActionToBack("DecrementStacks", () =>
        {
            DecrementStacks();
        });
    }

    public virtual void OnTargetedByCard(AbstractCard sourceCard)
    {

    }

    /// <summary>
    /// Returns how much more or less it costs to target this character with this card.
    /// </summary>
    public virtual int GetTargetedCostModifier(AbstractCard card)
    {
        return 0;
    }

    public virtual void Init()
    {

    }

    public void OnUnblockedDamageDealt()
    {

    }

}

public enum StatusEffectChange
{
    INCREASE, DECREASE
}

public enum StatusPolarityType
{
    BUFF,
    DEBUFF,
    NEITHER
}

public enum StatusLocalityType
{
    UNIT,
    GLOBAL
}