using System.Collections.Generic;
using System.Linq;

public class GameState
{
    public GameState()
    {
        Instance = this;

        Initialize();
    }

    public void Awake()
    {

    }

    public static GameState Instance = new GameState();
    public void Initialize()
    {
        var totalDeck = new List<AbstractCard>
        {
        };
        var deck = Deck;
        totalDeck.ForEach(item => deck.AddNewCardToDiscardPile(item));

    }
    // current RUN state

    /// <summary>
    /// Battles start at turn ZERO, and iterate at the start of each new turn.
    /// </summary>
    public int BattleTurn { get; set; } = 0;
    public BattleDeck Deck { get; set; } = new BattleDeck();

    public List<AbstractCard> CardInventory { get; set; } = new List<AbstractCard>
    {
        new Bayonet()
    };

    public List<AbstractSoldierPerk> AugmentationInventory { get; set; } = new List<AbstractSoldierPerk>
    {
        new AgilePerk()
    };

    public int NumCardsPlayedThisTurn { get; set; } = 0;

    public List<AbstractStatusEffect> GlobalBattleStatusEffects = new List<AbstractStatusEffect>();


    public int Credits { get; set; } = 0;
    public int energy { get; set; } = 3;
    public int maxEnergy { get; set; } = 3;
    public List<AbstractBattleUnit> PersistentCharacterRoster => GameState.Roster;
    public List<AbstractBattleUnit> AllyUnitsInBattle { get; set; } = new List<AbstractBattleUnit>();
    public List<AbstractBattleUnit> EnemyUnitsInBattle { get; set; } = new List<AbstractBattleUnit>();

    public List<AbstractStatusEffect> AllAlliedStatusEffects => AllyUnitsInBattle.SelectMany(item => item.StatusEffects).ToList();
    public AbstractMission CurrentMission { get; set; }

    /// <summary>
    /// List of global battle mechanics for the mission.  Note that this list is emptied at the start of each mission.
    /// </summary>
    public List<AbstractGlobalBattleMechanic> GlobalBattleMechanics = new List<AbstractGlobalBattleMechanic>();

    public ShopData ShopData { get; } = new ShopData();

    public DoomCounter DoomCounter { get; } = new DoomCounter();
    public StrategicDetails StrategicDetails { get; } = new StrategicDetails();

    public int cardsPlayedThisTurn = 0;

    #region UI State
    public AbstractBattleUnit CharacterSelected { get; set; }
    public int Day { get; set; } = 1;
    public int Act { get; set; } = 1;
    public List<AbstractBattleUnit> AllyUnitsSentOnRun { get; set; } = new List<AbstractBattleUnit>();
    public static List<AbstractBattleUnit> Roster { get; private set; }

    public List<AbstractMissionReward> AdditionalRewardsAccrued = new List<AbstractMissionReward>();

    #endregion

    #region Convenience


    public List<AbstractBattleUnit> GetUnitsAttackingUnit(AbstractBattleUnit target)
    {
        var enemiesAttackingTarget = EnemyUnitsInBattle.Where(enemy => enemy.CurrentIntents.Any(intent => IsIntentAttackingMe(intent, target)));
        return enemiesAttackingTarget.ToList();
    }

    private bool IsIntentAttackingMe(AbstractIntent intent, AbstractBattleUnit unitBeingAttacked)
    {
        if (intent is SingleUnitAttackIntent)
        {
            var singleUnitAttack = intent as SingleUnitAttackIntent;
            if (singleUnitAttack.Target == unitBeingAttacked)
            {
                return true;
            }
        }
        return false;
    }

    #endregion
}

/// <summary>
/// These represent triggers/action hooks that aren't associated to any specific character in battle.
/// Examples: Retreating mechanic, most mission modifiers
/// </summary>
public abstract class AbstractGlobalBattleMechanic
{
    public int Stacks { get; set; }

    public virtual void OnTurnStart()
    {

    }

    public virtual void OnCardPlayed()
    {

    }
}


public class DoomCounter
{
    public void AdvanceDoomCounterForDay()
    {
        CurrentDoomCounter += 2;
    }

    #region obsolete
    /// <summary>
    /// This works similarly to how risk of rain represents character damage.  There's a base level dictated by the Doom Counter, and this is multiplied by whatever percentage to 
    /// get the actual damage for the creature this combat enocounter.
    /// </summary>
    public int GetAdjustedDamage(int percent)
    {
        var perc = (float)percent;
        perc = perc / 100;
        return (int)(perc * GetCurrentDoomLevel().BaseDamage);
    }

    /// <summary>
    /// This works similarly to how risk of rain represents character damage.  There's a base level dictated by the Doom Counter, and this is multiplied by whatever percentage to 
    /// get the actual hp for the creature this combat enocounter.
    /// </summary>
    public int GetAdjustedHp(int percent)
    {
        var perc = (float)percent;
        perc = perc / 100;
        return (int)(perc * GetCurrentDoomLevel().BaseDamage);
    }

    #endregion

    public int CurrentDoomCounter { get; set; } = 0;

    public DoomLevel GetCurrentDoomLevel()
    {
        if (CurrentDoomCounter < 20)
        {
            return DoomLevel.EASY;
        }
        else if (CurrentDoomCounter < 40)
        {
            return DoomLevel.MEDIUM;
        }
        else if (CurrentDoomCounter < 60)
        {
            return DoomLevel.HARD;
        }
        else if (CurrentDoomCounter < 80)
        {
            return DoomLevel.VERY_HARD;
        }
        else if (CurrentDoomCounter < 100)
        {
            return DoomLevel.OH_NO;
        }
        return DoomLevel.DATA_EXPUNGED;
    }
}

public class DoomLevel
{
    public int BaseHealth { get; set; }
    public int BaseDamage { get; set; }
    public string Description { get; set; }
    public static DoomLevel EASY = new DoomLevel
    {
        BaseHealth = 40,
        BaseDamage = 10,
        Description = "Easy"
    };
    public static DoomLevel MEDIUM = new DoomLevel
    {
        BaseHealth = 50,
        BaseDamage = 12,
        Description = "Medium"
    };
    public static DoomLevel HARD = new DoomLevel
    {
        BaseHealth = 60,
        BaseDamage = 14,
        Description = "Hard"
    };
    public static DoomLevel VERY_HARD = new DoomLevel
    {
        BaseHealth = 70,
        BaseDamage = 16,
        Description = "Very Hard"
    };
    public static DoomLevel OH_NO = new DoomLevel
    {
        BaseHealth = 80,
        BaseDamage = 20,
        Description = "Oh No"
    };
    public static DoomLevel DATA_EXPUNGED = new DoomLevel
    {
        BaseHealth = 90,
        BaseDamage = 25,
        Description = "[DATA EXPUNGED]"
    };
}

public class ShopData
{

}