using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class BuffSelfIntent : SimpleIntent
{
    public int Stacks;

    public BuffSelfIntent(AbstractBattleUnit self,
        AbstractStatusEffect statusEffect,
        int stacks = 1): 
        base(self,
            IntentIcons.BuffIntent)
    {
        Stacks = stacks;
        this.StatusEffect = statusEffect;
    }

    public AbstractStatusEffect StatusEffect { get; private set; }

    public override string GetGenericDescription()
    {
        return "This unit will buff itself next turn.";
    }

    protected override void Execute()
    {
        ActionManager.Instance.ApplyStatusEffect(Source, StatusEffect, Stacks);
    }
}

public class BuffOtherIntent : SimpleIntent
{
    public int Stacks;

    public Func<AbstractBattleUnit> BattleUnitDeciderFunction;

    public BuffOtherIntent(AbstractBattleUnit self,
        AbstractStatusEffect statusEffect,
        int stacks = 1) :
        base(self,
            IntentIcons.BuffIntent)
    {
        Stacks = stacks;
        this.StatusEffect = statusEffect;
        // Just picks another unit in the party at random
        BattleUnitDeciderFunction = () => GameState.Instance.EnemyUnitsInBattle.Where(item => item != Source).PickRandom();
    }

    public AbstractStatusEffect StatusEffect { get; private set; }

    public override string GetGenericDescription()
    {
        return "This unit will buff another unit next turn.";
    }

    protected override void Execute()
    {
        var otherEnemies = GameState.Instance.EnemyUnitsInBattle.Where(item => item != Source);
        if (!otherEnemies.Any())
        {
            ActionManager.Instance.ApplyStatusEffect(Source, StatusEffect, Stacks);
        }
        else
        {
            ActionManager.Instance.ApplyStatusEffect(BattleUnitDeciderFunction(), StatusEffect, Stacks);
        }
    }
}
