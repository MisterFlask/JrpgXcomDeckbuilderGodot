using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Assets.CodeAssets.ParticleSystemEffects;

public static class QueuedActions
{



    /// <summary>
    /// REQUIRED that the IsCurrentActionFinished flag be set by the end of the action.
    /// </summary>
    public static void DelayedActionWithCustomTrigger(string name, 
        Action action,
        QueueingType queueingType = QueueingType.TO_BACK)
    {
        PushActionOntoRelevantQueue(new BasicDelayedAction(action, ActionManager.Instance.ActionCurrentlyBeingPerformed, name),
            queueingType);
    }
    public static void DelayedActionWithSpecialEffects(string name,
        Action action,
        Func<List<SpecialEffect>> retrieveEffects,
        QueueingType queueingType = QueueingType.TO_BACK)
    {
        var actionToDo = new BasicDelayedAction(action, ActionManager.Instance.ActionCurrentlyBeingPerformed, name);
        actionToDo.EffectsToWaitOn = retrieveEffects();
        actionToDo.EffectsToWaitOn.ForEach(item => item.BeginSpecialEffect());
        PushActionOntoRelevantQueue(actionToDo,
            queueingType);
    }


    private static void PushActionOntoRelevantQueue(BasicDelayedAction action, QueueingType queueingType)
    {
        var parentAction = ActionManager.Instance.ActionCurrentlyBeingPerformed;

        if (queueingType == QueueingType.TO_BACK)
        {
            parentAction.ChildActionsQueue.Add(action);
        }
        else
        {
            parentAction.ChildActionsQueue.AddToFront(action);
        }
    }

    public static void DelayedActionWithFinishTrigger(string name, Action action, Func<bool> finishTrigger, QueueingType queueingType = QueueingType.TO_BACK)
    {
        PushActionOntoRelevantQueue(new DelayedActionWithFinishTrigger(name, action, ActionManager.Instance.ActionCurrentlyBeingPerformed, finishTrigger),
            queueingType);
    }

    public static void ImmediateAction(string name, Action action, QueueingType queueingType = QueueingType.TO_BACK)
    {
        PushActionOntoRelevantQueue(new ImmediateAction( action, ActionManager.Instance.ActionCurrentlyBeingPerformed, name:name),
            queueingType);
    }
}
public enum QueueingType
{
    TO_FRONT,
    TO_BACK
}