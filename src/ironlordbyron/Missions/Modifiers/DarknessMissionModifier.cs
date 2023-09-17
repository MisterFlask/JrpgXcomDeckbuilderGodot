using UnityEngine;
using System.Collections;

public class DarknessMissionModifier : MissionModifier
{
    public override string Description()
    {
        return "A strange darkness pervades this place.  Stress gain is increased during this mission.";
    }

    public override void OnMissionCombatBegins()
    {
        var characters = GameState.Instance.AllyUnitsInBattle;
        foreach(var c in characters)
        {
            ActionManager.Instance.ApplyStatusEffect(c, new DarknessStatusEffect(), 2);
        }
    }

    public override int IncrementalMoney()
    {
        return 40;
    }
}

public class DarknessStatusEffect : AbstractStatusEffect
{
    public DarknessStatusEffect()
    {
        Name = "Darkness";
    }

    public override string Description => "Whenever Stress is applied to this character, it is increased by [stacks].";
}