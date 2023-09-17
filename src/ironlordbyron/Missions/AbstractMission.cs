using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public abstract class AbstractMission 
{

    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon();
    public List<MissionModifier> MissionModifiers { get; set; } = new List<MissionModifier>();

    public static string GenerateMissionName()
    {
        return "Operation " + WordLists.GetRandomCommonAdjective() + " " + WordLists.GetRandomCommonNoun();
    }

    public string GenerateMissionDescriptiveText()
    {
        var start = $"Kill enemies for a reward!  Rewards include: ";
        foreach(var reward in Rewards)
        {
            start += Environment.NewLine + "*" + reward.GenericDescription();
        }

        var enemiesText = this.EnemySquad.Description;

        start += Environment.NewLine + $"Foes: {enemiesText}\n";
        // start += Environment.NewLine + "Days left: " + DaysUntilExpiration;
        return start;
    }

    /// <summary>
    /// This exists to support missions whose "reward" is
    /// stopping the boss (gateway) mission from becoming harder
    /// (e.g. getting additional minions or buffs at combat start.)
    /// </summary>
    public virtual void OnGatewayMissionStartIfThisMissionStillActive()
    {

    }

    internal static ProtoGameSprite RetrieveIconFromMissionIconFolder(string name)
    {
        return ProtoGameSprite.MissionIcon(name);
    }



    public string Name { get; set; }
    public int Difficulty { get; set; } // 1 to 5

    //After this many days, the mission can no longer be performed.
    public int DaysUntilExpiration { get; set; } = 4;

    public List<AbstractMissionReward> Rewards { get; set; } = new List<AbstractMissionReward>();

    public virtual void OnFailed()
    {

    }

    public virtual void OnSuccess()
    {
        foreach (var reward in Rewards)
        {
            reward.OnReward();
        }
    }
    public virtual bool IsFailed()
    {
        return GameState.Instance.AllyUnitsInBattle.TrueForAll(item => item.IsDead);
    }

    public virtual void OnStartOfBattle()
    {

    }

    public virtual bool CanGoOnMission()
    {
        return true;
    }

    public Squad EnemySquad { get; set; }

    public int MaxNumberOfFriendlyCharacters { get; set; } = 3;

    public bool IsFailure { get; set; } = false;
    public bool IsVictory { get; set; } = false;

    public MissionTerrain Terrain { get; set; } = MissionTerrain.FOREST;
    public ProtoGameSprite BattleBackground { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Backgrounds/Battleback1");

    public void Init()
    {
        var incrementalGoldFromModifiers = MissionModifiers.Sum(item => item.IncrementalMoney());

        if (incrementalGoldFromModifiers > 0)
        {
            var goldRewardAlreadyExisting = Rewards.FirstOrDefault(item => item is GoldMissionReward);
            if (goldRewardAlreadyExisting != null)
            {
                var goldReward = goldRewardAlreadyExisting as GoldMissionReward;
                goldReward.MoneyEarned += incrementalGoldFromModifiers;
            }
            else
            {
                Rewards.Add(new GoldMissionReward(incrementalGoldFromModifiers));
            }
        }
    }
}

public class MissionTerrain
{
    public static MissionTerrain FOREST = new MissionTerrain
    {
        TerrainImage = ProtoGameSprite.TerrainIcon("forest"),
        TerrainName = "Forest"
    };

    public static MissionTerrain CITY = new MissionTerrain
    {
        TerrainImage = ProtoGameSprite.TerrainIcon("modern-city"),
        TerrainName = "City"
    };

    public static List<MissionTerrain> TerrainTypes = new List<MissionTerrain>
    {
        FOREST,CITY
    };

    public ProtoGameSprite TerrainImage { get; set; } = ProtoGameSprite.TerrainIcon("forest");

    public string TerrainName { get; set; } = "Default";
}