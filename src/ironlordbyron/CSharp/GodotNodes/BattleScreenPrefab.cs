using Godot;
using System.Collections.Generic;
using System.Linq;

public class BattleScreenPrefab : Node2D
{
    public static BattleScreenPrefab INSTANCE;
    public BattleScreenPrefab()
    {
    }

    public override void _Ready()
    {
        // Initialization logic goes here
        INSTANCE = this;/* 
        SelectCardInHandOverlay.Hide();
        ShowDeckScreen.Hide();

        UtilityObjectHolder utilityObjectHolder = (UtilityObjectHolder)GetNode("/root/UtilityObjectHolder"); */
        // utilityObjectHolder.Start();
    }
    private GameState state => ServiceLocator.GameState();
    private ActionManager action => ServiceLocator.GetActionManager();
    private List<BattleUnitPrefab> PotentialBattleEntityEnemySpots;
    private List<BattleUnitPrefab> PotentialBattleEntityLargeEnemySpots;
    private List<BattleUnitPrefab> PotentialBattleEntityHugeEnemySpots;
    private List<BattleUnitPrefab> PotentialBattleEntityAllySpots;

    public Node2D EnemyUnitSpotsParent;
    public Node2D LargeEnemyUnitSpotsParent;
    public Node2D HugeEnemyUnitSpotsParent;
    public Node2D AllyUnitSpotsParent;

    public Image Image;

    // ...
    public static AbstractIntent IntentMousedOver { get; set; }
    public static AbstractBattleUnit BattleUnitMousedOver { get; set; }
    public static List<AbstractCard> CardsMousedOver { get; set; } = new List<AbstractCard>();

    private List<int> ForbiddenSmallEnemyIndicesIfLargeEnemyExists = new List<int>
    {
        0,1,2,3,4,5
    };

    private List<int> ForbiddenSmallEnemyIndicesIfMediumEnemyExistsInSlotOne = new List<int>
    {
        0,1,3,4
    };

    private List<int> ForbiddenSmallEnemyIndicesIfMediumEnemyExistsInSlotTwo = new List<int>
    {
        6,7,9,10
    };

    public List<int> GetForbiddenIndicesForSmallCharacters_BasedOnExistingPrefabPopulations()
    {
        var forbiddenSmallSlots = new List<int>();

        var mediumEnemyInSlotOne = PotentialBattleEntityLargeEnemySpots[0].UnderlyingEntity != null;
        var mediumEnemyInSlotTwo = PotentialBattleEntityLargeEnemySpots[1].UnderlyingEntity != null;
        var largeEnemyExists = PotentialBattleEntityHugeEnemySpots[0].UnderlyingEntity != null;

        if (mediumEnemyInSlotOne)
        {
            forbiddenSmallSlots.AddRange(ForbiddenSmallEnemyIndicesIfMediumEnemyExistsInSlotOne);
        }
        if (mediumEnemyInSlotTwo)
        {
            forbiddenSmallSlots.AddRange(ForbiddenSmallEnemyIndicesIfMediumEnemyExistsInSlotTwo);
        }
        if (largeEnemyExists)
        {
            forbiddenSmallSlots.AddRange(ForbiddenSmallEnemyIndicesIfLargeEnemyExists);
        }

        return forbiddenSmallSlots;
    }
    // ...

    public List<BattleUnitPrefab> GetAvailableSpotsForNewSmallUnits()
    {
        var emptySpots = PotentialBattleEntityEnemySpots.Where(item => item.UnderlyingEntity == null);
        var forbiddenIndices = GetForbiddenIndicesForSmallCharacters_BasedOnExistingPrefabPopulations();

        return emptySpots
            .Where(item => !forbiddenIndices.Contains(PotentialBattleEntityEnemySpots.IndexOf(item)))
            .ToList();
    }

    public void SetupAlliesAndEnemies(List<AbstractBattleUnit> StartingEnemies, List<AbstractBattleUnit> StartingAllies)
    {
        var smallEnemies = StartingEnemies.Where(item => item.UnitSize == UnitSize.SMALL).ToList();
        var mediumEnemies = StartingEnemies.Where(item => item.UnitSize == UnitSize.MEDIUM).ToList();
        var largeEnemies = StartingEnemies.Where(item => item.UnitSize == UnitSize.LARGE).ToList();

        var forbiddenSmallEnemyIndices = new List<int>();
        var forbiddenMediumEnemyIndices = new List<int>();
        if (mediumEnemies.Count >= 1)
        {
            forbiddenSmallEnemyIndices.AddRange(ForbiddenSmallEnemyIndicesIfMediumEnemyExistsInSlotOne);
        }
        if (mediumEnemies.Count == 2)
        {
            forbiddenSmallEnemyIndices.AddRange(ForbiddenSmallEnemyIndicesIfMediumEnemyExistsInSlotTwo);
        }
        if (largeEnemies.Count == 1)
        {
            forbiddenSmallEnemyIndices.AddRange(ForbiddenSmallEnemyIndicesIfLargeEnemyExists);
            forbiddenMediumEnemyIndices.Add(0);
        }

        // Error handling and sprite setting...
        // ...

        for (int i = 0; i < StartingAllies.Count; i++)
        {
            PotentialBattleEntityAllySpots[i].Initialize(StartingAllies[i]);
        }
        for (int i = 0; i < smallEnemies.Count; i++)
        {
            //acceptableStartingSmallEnemySpots[i].Initialize(smallEnemies[i]);
        }
        // Initialize medium and large enemies...
        // ...
    }

    // ...

    // ...

    public bool IsRoomForAnotherEnemy()
    {
        return GetAvailableSpotsForNewSmallUnits().Any();
    }

    public CreateEnemyResult CreateNewEnemyAndRegisterWithGamestate(AbstractBattleUnit battleUnit)
    {
        var firstEmptyBattleUnitHolder = GetAvailableSpotsForNewSmallUnits()[0];
        if (firstEmptyBattleUnitHolder == null)
        {
            return new CreateEnemyResult
            {
                FailedDueToNoSpaceLeft = true
            };
        }

        firstEmptyBattleUnitHolder.Initialize(battleUnit);
        GameState.Instance.EnemyUnitsInBattle.Add(battleUnit);
        return new CreateEnemyResult();
    }

    // ...
    // ...

    bool battleStarted = false;

    public override void _Process(float delta)
    {
        SetupBattle();
    }

    private void SetupBattle()
    {
        if (!battleStarted)
        {
            if (state.AllyUnitsInBattle == null || !state.AllyUnitsInBattle.Any())
            {
                GD.PrintErr("No allies in battle!");
                return;
            }

            state.Deck = new BattleDeck();
            foreach (var character in state.AllyUnitsInBattle)
            {
                character.InitForBattle();
                foreach (var card in character.BattleDeck)
                {
                    state.Deck.AddNewCardToDiscardPile(card.CopyCard(logicallyIdenticalToExistingCard: true));
                }
            }

            action.DrawCards(5);

            // Setup allies and enemies and initialization of units...

            // ... (previous code)

            if (!battleStarted)
            {
                // ... (previous code)

                /* SetupAlliesAndEnemies(ServiceLocator.GameState().EnemyUnitsInBattle, ServiceLocator.GameState().AllyUnitsInBattle);

                PotentialBattleEntityAllySpots.ForEach(item => item.HideOrShowAsAppropriate());
                PotentialBattleEntityEnemySpots.ForEach(item => item.HideOrShowAsAppropriate());
                PotentialBattleEntityLargeEnemySpots.ForEach(item => item.HideOrShowAsAppropriate());
                PotentialBattleEntityHugeEnemySpots.ForEach(item => item.HideOrShowAsAppropriate());

                state.EnemyUnitsInBattle.ForEach(item => item.InitForBattle());
                state.AllyUnitsInBattle.ForEach(item => item.InitForBattle());

                BattleStarter.StartBattle(this);
                battleStarted = true; */
            }
        }
    }
}
public class CreateEnemyResult
{
    public bool FailedDueToNoSpaceLeft = false;
    public bool Successful => !FailedDueToNoSpaceLeft;
}

// ...
