using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractBattleUnit
{
    public UnitSize UnitSize { get; set; } = UnitSize.SMALL;

    public string UniqueId = Guid.NewGuid().ToString();
    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon();

    public bool HasStatusEffect<T>() where T : AbstractStatusEffect
    {
        return StatusEffects.Any(item => item.GetType() == typeof(T));
    }

    public int CombatsParticipatedIn = 0;

    public bool PortraitImage { get; set; } = false;
    public bool PromotionAvailable => this.SoldierClass is RookieClass && CombatsParticipatedIn > 0;

    public int MaxHp { get; set; } = 10;
    public int NumberCardRewardsEligibleFor { get; set; } = 0;

    public string Description { get; set; }

    public bool IsDead => CurrentHp <= 0;
    public int CurrentLevel { get; set; } = 1;
    public int CurrentBlock { get; set; }
    public int CurrentHp { get; set; }
    public string CharacterFirstName { get; set; }
    public string CharacterLastName { get; set; }
    public int PerDayHealingRate { get; set; } = 2;

    public int CurrentStress => this.GetStatusEffect<StressStatusEffect>()?.Stacks ?? 0;
    public int MaxStress = 100;
    public int PerDayStressHealingRate = 5;

    public int CurrentFatigue { get; set; } = 4;
    public int MaxFatigue { get; set; } = 4;


    public AbstractSoldierClass SoldierClass { get; protected set; } = new RookieClass();
    public string UnitClassName => SoldierClass.Name();

    public string CharacterFullName { get; set; } = "";

    public string CharacterNicknameOrEnemyName { get; set; } = "";

    public List<AbstractStatusEffect> StatusEffects { get; set; } = new List<AbstractStatusEffect>();

    public BattleUnitPrefab CorrespondingPrefab { get; set; }

    public bool IsAlly { get; set; }

    public bool IsEnemy { get => !IsAlly; }

    public bool IsElite { get; set; } = false;

    public bool IsBoss { get; set; } = false;

    // Expected to be empty for enemies
    public List<AbstractCard> StartingCardsInDeck { get; set; } = new List<AbstractCard>();

    private List<AbstractCard> _CardsInPersistentDeck { get; set; } = new List<AbstractCard>();

    public IEnumerable<AbstractCard> CardsInPersistentDeck => _CardsInPersistentDeck;

    public void ApplyAugmentation(AbstractSoldierPerk aug)
    {
        aug.Owner = this;
        aug.OnAssignment(this);
        Perks.Add(aug);
    }

    public EnemyFaction EnemyFaction { get; set; } = EnemyFaction.NONE;

    public EnemySquadRole SquadRole { get; set; } = EnemySquadRole.REGULAR;

    public void RemoveCardsFromPersistentDeck(IEnumerable<AbstractCard> cardsToRemove)
    {
        _CardsInPersistentDeck.RemoveAll(item => cardsToRemove.Contains(item));

    }
    public void RemoveCardsFromPersistentDeckByType<T>() where T : AbstractCard
    {
        _CardsInPersistentDeck.RemoveAll(item => item.GetType() == typeof(T));

    }

    public List<AbstractCard> BattleDeck { get; set; } = new List<AbstractCard>();

    public List<AbstractSoldierPerk> Perks { get; } = new List<AbstractSoldierPerk>();

    public bool HasDied { get; set; } = false;

    public void InitializePersistentDeck()
    {
        _CardsInPersistentDeck = new List<AbstractCard>();
        AddCardsToPersistentDeck(StartingCardsInDeck);
    }

    public void ApplyStatusEffect<T>(T effect, int stacks = 1) where T : AbstractStatusEffect
    {
        if (HasStatusEffect<T>() && !GetStatusEffect<T>().Stackable)
        {
            return;
        }

        if (HasStatusEffect<T>())
        {
            GetStatusEffect<T>().Stacks += stacks;
            if (GetStatusEffect<T>().Stacks < 0 && !GetStatusEffect<T>().AllowedToGoNegative)
            {
                RemoveStatusEffect<T>();
            }
            else
            {
                if (stacks > 0)
                {
                    BattleRules.ProcessHooksWhenStatusEffectAppliedToUnit(this, effect, stacks);
                }
            }
        }
        else
        {
            effect.AssignOwner(this);
            effect.Stacks = stacks;
            StatusEffects.Add(effect);
            BattleRules.ProcessHooksWhenStatusEffectAppliedToUnit(this, effect, stacks);
        }

        if (GetStatusEffect<T>() != null && GetStatusEffect<T>().Stacks == 0)
        {
            RemoveStatusEffect<T>();
        }
    }

    internal bool IsTargetable()
    {
        return !IsDead;
    }

    public AbstractStatusEffect GetStatusEffect<T>()
    {
        return this.StatusEffects.Where(item => item is T).FirstOrDefault();
    }

    public void AddCardsToPersistentDeck(IEnumerable<AbstractCard> cards)
    {
        foreach (var baseCard in cards)
        {
            AddCardToPersistentDeck(baseCard);
        }
    }

    public void AddCardToPersistentDeck(AbstractCard baseCard)
    {
        var card = baseCard.CopyCard();
        card.Owner = this;
        _CardsInPersistentDeck.Add(card);
    }

    public abstract List<AbstractIntent> GetNextIntents();

    public bool IsAiControlled = true;

    public List<AbstractIntent> CurrentIntents = new List<AbstractIntent>();

    public int Turn { get; set; } = 1;

    public bool IsAdvanced => HasStatusEffect<AdvancedStatusEffect>();

    public List<AbstractIntent> NextIntentOverride { get; set; }


    /// <summary>
    /// This happens AFTER all enemies have executed intents.
    /// </summary>
    public void OnTurnStart()
    {
        ActionManager.Instance.PushActionToBack("AbstractBattleUnit_OnTurnStart", () =>
        {

            CurrentBlock = 0;
            if (CurrentFatigue < MaxFatigue)
            {
                this.CurrentFatigue += 1;
            }
        });
    }

    /// <summary>
    /// This occurs whenever the player hits the "end turn" button
    /// </summary>
    public void OnTurnEnd()
    {
        foreach (var statusEffect in StatusEffects)
        {
            statusEffect.OnTurnEnd();
        }
    }


    public void ExecuteOnIntentIfAvailable()
    {
        if (IsDead)
        {
            CurrentIntents = null;
            return;
        }

        Turn++;
        if (IsAiControlled)
        {
            if (CurrentIntents != null)
            {
                foreach (var intent in CurrentIntents)
                {
                    intent.ExecuteIntent();
                }
            }
            if (NextIntentOverride != null)
            {
                CurrentIntents = NextIntentOverride;
                NextIntentOverride = null;
            }
            CurrentIntents = GetNextIntents();
        }
    }

    public void InitForBattle()
    {
        this.CurrentFatigue = MaxFatigue;
        if (IsEnemy)
        {
            this.CurrentHp = MaxHp;
        }
        this.CurrentIntents = GetNextIntents();
        BattleDeck = new List<AbstractCard>();
        BattleDeck.AddRange(CardsInPersistentDeck);
    }

    public AbstractBattleUnit CloneUnit()
    {
        var copy = (AbstractBattleUnit)this.MemberwiseClone();
        copy.UniqueId = Guid.NewGuid().ToString();
        copy.InitializePersistentDeck();
        var newName = CharacterNameGenerator.GenerateCharacterName();
        copy.CurrentFatigue = MaxFatigue;
        copy.CurrentHp = MaxHp;
        copy.CharacterFirstName = newName.FirstName;
        copy.CharacterLastName = newName.LastName;
        copy.CharacterFullName = newName.FirstName + " " + newName.LastName;
        if (!(this is AbstractEnemyUnit))
        {
            copy.CharacterNicknameOrEnemyName = newName.Nickname;
        }
        return copy;
    }


    #region convenience functions
    protected ActionManager action()
    {
        return ServiceLocator.GetActionManager();
    }

    protected List<AbstractBattleUnit> enemies()
    {
        return ServiceLocator.GameState().EnemyUnitsInBattle;
    }
    protected List<AbstractBattleUnit> allAlliedUnits()
    {
        return ServiceLocator.GameState().AllyUnitsInBattle;
    }

    protected GameState state()
    {
        return ServiceLocator.GameState();
    }

    /// <summary>
    /// This is just a cleanup operation.
    /// </summary>
    public void PerformStateBasedActions()
    {
        foreach (var effect in StatusEffects.ToList())
        {
            if (effect.Stacks == 0)
            {
                StatusEffects.Remove(effect);
            }
        }
        BattleRules.CheckAndRegisterDeath(this, null, null);

        // kind of a hack, but should be fine
        foreach (var statusEffect in StatusEffects.ToList())
        {
            if (!StatusEffects.Contains(statusEffect))
            {
                continue;
            }

            var effectsOfThisType = StatusEffects.Where(item => item.GetType() == statusEffect.GetType());
            if (effectsOfThisType.Count() > 1)
            {
                var consolidatedStacks = effectsOfThisType.Sum(item => item.Stacks);
                StatusEffects.RemoveAll(item => item.GetType() == statusEffect.GetType());
                StatusEffects.Add(statusEffect);
                statusEffect.Stacks = consolidatedStacks;
            }
        }
    }

    public virtual void OnCombatStart()
    {

    }

    public void TickDownStatusEffect<T>() where T : AbstractStatusEffect
    {
        RemoveStatusEffect<T>(1);
    }

    /// <summary>
    /// If stacksToRemove is null, removes all stacks.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stacksToRemove"></param>
    public void RemoveStatusEffect<T>(int? stacksToRemove = null) where T : AbstractStatusEffect
    {
        if (this.HasStatusEffect<T>())
        {
            var attribute = this.StatusEffects.First(item => item is T);
            if (stacksToRemove == null)
            {
                this.StatusEffects.Remove(attribute);
            }
            else
            {
                attribute.Stacks -= stacksToRemove ?? 0;
            }
        }
    }

    public void ApplySoldierPerk(AbstractSoldierPerk perk, int stacks = 1)
    {
        perk.Stacks = stacks;
        Perks.Add(perk);
    }
    public void RemoveSoldierPerk<T>() where T : AbstractSoldierPerk
    {
        Perks.RemoveAll(item => item.GetType() == typeof(T));
    }

    public void RemoveSoldierPerkByType(Type t)
    {
        Perks.RemoveAll(item => item.GetType() == t);
    }

    public void ChangeClass(AbstractSoldierClass newClass)
    {
        this.SoldierClass = newClass;
    }

    private bool difficultyInitialized = false;
    public virtual void SetDifficulty(int difficulty)
    {
        if (difficultyInitialized)
        {
            throw new Exception("Initialized difficulty twice!");
        }
        difficultyInitialized = true;
        ApplyStatusEffect(new StrengthStatusEffect(), difficulty);
    }
    #endregion


    public BattleUnitStatisticsInThisCombat StatsForThisCombat = new BattleUnitStatisticsInThisCombat();

    public void LevelUp()
    {
        CurrentLevel++;
        NumberCardRewardsEligibleFor++;
        MaxHp += 2;
        CurrentHp += 2;
    }

    public void Heal(int amount)
    {
        this.CurrentHp += amount;
        if (CurrentHp > MaxHp)
        {
            CurrentHp = MaxHp;
        }
    }

    public void ModifyStress(int amount)
    {
        this.ApplyStatusEffect<StressStatusEffect>(new StressStatusEffect(), stacks: amount);
    }

    public string GetDisplayName(DisplayNameType type)
    {
        if (this is AbstractEnemyUnit)
        {
            return CharacterNicknameOrEnemyName;
        }

        if (type == DisplayNameType.FULL_NAME)
        {
            if (CurrentLevel > 1)
            {
                return $"{CharacterFirstName} \"{CharacterNicknameOrEnemyName}\" {CharacterLastName}";
            }
            else
            {
                return $"{CharacterFirstName} {CharacterLastName}";
            }
        }

        if (type == DisplayNameType.SHORT_NAME)
        {
            if (CurrentLevel > 1)
            {
                return CharacterNicknameOrEnemyName;
            }
            else
            {
                return CharacterFirstName;
            }
        }
        return CharacterFirstName;
    }

    public virtual void AssignStatusEffectsOnCombatStart()
    {

    }

}


public enum DisplayNameType
{
    FULL_NAME,
    SHORT_NAME
}
public class BattleUnitStatisticsInThisCombat
{
    int AmountOfDamageTaken = 0;
    int NumberOfTimesStruck = 0;
}

public class UnitSize
{
    public string Name { get; set; }

    public static UnitSize SMALL = new UnitSize { Name = "small" };
    public static UnitSize MEDIUM = new UnitSize { Name = "medium" };
    public static UnitSize LARGE = new UnitSize { Name = "large" };
}