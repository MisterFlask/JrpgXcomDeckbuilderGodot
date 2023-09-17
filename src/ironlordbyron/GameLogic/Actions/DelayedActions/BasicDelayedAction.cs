using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Assets.CodeAssets.ParticleSystemEffects;
using System.Linq;

public class BasicDelayedAction 
{
    public string ActionName { get; set; }
    public ActionIdentifier Id;
    public bool IsStarted { get; set; }

    public virtual Action onStart { get; protected set; }

    public TimeSpan Timeout = TimeSpan.FromSeconds(20); // TODO

    public List<BasicDelayedAction> ChildActionsQueue = new List<BasicDelayedAction>();

    public BasicDelayedAction Parent = null;

    public StackTrace stackTrace;

    public bool IsTimeoutRelevant = true;

    public List<SpecialEffect> EffectsToWaitOn { get; set; } = new List<SpecialEffect>();
    public DateTime StartedOn { get; internal set; }

    public BasicDelayedAction(Action onStart, BasicDelayedAction parent, string name = "")
    {
        Id = new ActionIdentifier
        {
            Name = name,

        };
        stackTrace = new StackTrace();
        ActionName = name;
        this.onStart = onStart;
        if (parent != null)
        {
            Parent = parent;
            if (!parent.ChildActionsQueue.Contains(this))
            {
                parent.ChildActionsQueue.Add(this);
            }
        }
    }

    public void DeclareFinished()
    {
        ServiceLocator.GetActionManager().IsCurrentActionFinished = true;
    }

    public virtual bool IsFinished()
    {
        return ServiceLocator.GetActionManager().IsCurrentActionFinished 
            && EffectsToWaitOn.All(item => item.IsFinished()); // this is a flag this is required to set.
    }
}


public class DelayedActionWithFinishTrigger : BasicDelayedAction
{
    Func<bool> IsFinishedFunction;

    public DelayedActionWithFinishTrigger(string name, Action toPerform, BasicDelayedAction parent, Func<bool> isFinished) : base(toPerform, parent)
    {
        onStart = toPerform;
        ActionName = name;
        IsFinishedFunction = isFinished;
    }

    public override bool IsFinished()
    {
        return IsFinishedFunction();
    }
}


public class ActionIdentifier
{


    public Guid Id { get; set; }
    public string Name { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is ActionIdentifier other)
        {
            return Id == other.Id && Name == other.Name;
        }
        return false;
    }

    public override int GetHashCode()
    {
        unchecked // Allows overflow, which is fine for hash codes
        {
            int hash = 17;
            hash = hash * 23 + Id.GetHashCode();
            hash = hash * 23 + (Name?.GetHashCode() ?? 0);
            return hash;
        }
    }

    public override string ToString()
    {
        return $"Id:{Id};Name:{Name}";
    }
}
