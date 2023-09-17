using UnityEngine;
using System.Collections;
using System;

public class ImmediateAction: BasicDelayedAction
{
    public ImmediateAction(Action onStart, BasicDelayedAction parent, string name = "") : base(onStart, parent, name)
    {
    }
    public override bool IsFinished()
    {
        return true;
    }
}
