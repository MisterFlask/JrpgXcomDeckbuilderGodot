using UnityEngine;
using System.Collections;

public abstract class MissionFailurePunishment 
{
    public abstract string Description();
    public abstract void OnFailure();
}
