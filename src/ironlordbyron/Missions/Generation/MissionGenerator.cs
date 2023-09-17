using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.CodeAssets.Utils;
using Assets.CodeAssets.StsMapScreen;

public static class MissionGenerator
{
    public static List<MissionModifier> GetRandomMissionModifiers()
    {
        return new List<MissionModifier>()
        {
            MissionModifier.GetRandomMissionModifier()
        };
    }
}


public class KillEnemiesMission: AbstractMission
{
    public string Description { get; set; }

    public string GetDescription()
    {
        if (Description == null)
        {
            var names = EnemySquad.Members.Select(item => item.CharacterFullName);
            return string.Join(",", names);
        }
        return Description;
    }
}

public class Squad
{
    public List<AbstractBattleUnit> Members { get; set; }

    /// <summary>
    /// difficulties are 1-5
    /// 1: first part of first act
    /// 2: second part of first act
    /// 3: first part of second act
    /// 4: second part of second act
    /// 5: boss of second act
    /// </summary>
    public int BaseDifficulty { get; set; } = 0;
    public string Description { get; set; } = "???";

    public List<int> ValidForDifficulties { get; set; } = new List<int>();

    public void SetDifficulty(int difficulty)
    {
        foreach(var guy in Members)
        {
            guy.SetDifficulty(difficulty - BaseDifficulty);
        }
    }

    internal Squad CopySquad()
    {
        return new Squad
        {
            BaseDifficulty = BaseDifficulty,
            Description = Description,
            Members = Members.Select(item => item.CloneUnit()).ToList()
        };
    }
}


public abstract class MissionModifier
{
    public static MissionModifier GetRandomMissionModifier()
    {
        return new List<MissionModifier>()
        {
            new DarknessMissionModifier(),
            new HighWindsMissionModifier(),
            new NoxiousGasesMissionModifier()
        }.PickRandom();
    }

    public virtual int IncrementalMoney()
    {
        return 0;
    }


    public abstract void OnMissionCombatBegins();
    public abstract string Description();
}



