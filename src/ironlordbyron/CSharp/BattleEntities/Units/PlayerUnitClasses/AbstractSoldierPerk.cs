using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// These represent PERSISTENT perks, as opposed to status effects, which are just for the combat.
/// Also corresponds to status effects.
/// Mostly stackable.
/// </summary>
public abstract class AbstractSoldierPerk
{
    public int Stacks { get; set; } = 1;
    public abstract string Name();
    public abstract string Description();
    public Rarity Rarity { get; set; }

    public ProtoGameSprite Sprite { get; set; } = ProtoGameSprite.Default;
    public AbstractBattleUnit Owner { get; internal set; }

    internal static AbstractSoldierPerk GetRandomAcquireableAugmentation()
    {
        throw new NotImplementedException();
    }

    public List<string> CompatibleSoldierGuids = new List<string>();
    public List<AbstractBattleUnit> CompatibleSoldiers => CompatibleSoldierGuids.ConvertGuidsToSoldiers();

    public List<DamageModifier> DamageModifiers = new List<DamageModifier>();

    public virtual void PerformAtBeginningOfCombat(AbstractBattleUnit soldierAffected)
    {

    }

    public virtual void PerformAtBeginningOfNewDay(AbstractBattleUnit soldierAffected)
    {

    }

    /// <summary>
    ///  This is run on cards added to the deck after perk is gained
    /// </summary>
    public virtual void ModifyCardsUponAcquisition(AbstractCard card, AbstractBattleUnit soldierAffected)
    {

    }

    public void AddStacks(AbstractBattleUnit soldierAffected, int stacks)
    {
        Stacks += stacks;
        if (Stacks <= 0)
        {
            soldierAffected.RemoveSoldierPerkByType(this.GetType());
        }
    }

    public void DecrementStacks(AbstractBattleUnit soldierAffected)
    {
        Stacks--;
        if (Stacks <= 0)
        {
            soldierAffected.RemoveSoldierPerkByType(this.GetType());
        }
    }


    public static AbstractSoldierPerk CreateGrantsStatusEffectPerk(
        string name,
        string description,
        AbstractStatusEffect effect,
        int stacks)
    {
        return new GrantsStatusEffectPerk(
        name,
        description,
        effect,
        stacks);
    }


    public virtual void OnAssignment(AbstractBattleUnit abstractBattleUnit)
    {

    }
    public virtual bool CanAssignToSoldier(AbstractBattleUnit soldier)
    {
        return true;
    }

    public AbstractSoldierPerk Clone()
    {
        var copy = (AbstractSoldierPerk)this.MemberwiseClone();
        return copy;
    }
}


public class GrantsCardStickerPerk : AbstractSoldierPerk
{
    private string GivenName { get; set; }
    private string GivenDescription { get; set; }
    private AbstractCardSticker Effect { get; set; }
    public GrantsCardStickerPerk(
        string name,
        string description,
        AbstractCardSticker effect,
        int stacks = 1,
        Rarity rarity = Rarity.NOT_IN_POOL)
    {
        GivenName = name;
        GivenDescription = description;
        Stacks = stacks;
        Rarity = rarity;
        Effect = effect;
    }

    public override void OnAssignment(AbstractBattleUnit abstractBattleUnit)
    {
        /*ShowDeckScreen.ShowMandatorySelectCardFromCharacterDeckScreen((cardSelected) =>
        {
            cardSelected.AddSticker(this.Effect);

        },
        () =>
        {
            throw new Exception("Cannot select card");
        },
        (card) =>
        {
            return Effect.IsCardTagApplicable(card);
        },
        prompt: "Adds effect to card: " + Effect.CardDescriptionAddendum());
        */
    }

    public override string Name()
    {
        return GivenName;
    }

    public override string Description()
    {
        return GivenDescription;
    }

    public override bool CanAssignToSoldier(AbstractBattleUnit soldier)
    {
        return soldier.CardsInPersistentDeck.Any(item => Effect.IsCardTagApplicable(item));
    }
}

public class GrantsStatusEffectPerk : AbstractSoldierPerk
{
    private string GivenName { get; set; }
    private string GivenDescription { get; set; }
    public AbstractStatusEffect Effect { get; set; }
    public GrantsStatusEffectPerk(
        string name,
        string description,
        AbstractStatusEffect effect,
        int stacks = 1,
        Rarity rarity = Rarity.NOT_IN_POOL)
    {
        GivenName = name;
        GivenDescription = description;
        Stacks = stacks;
        Effect = effect;
        Rarity = rarity;
    }

    public override void PerformAtBeginningOfCombat(AbstractBattleUnit soldierAffected)
    {
        soldierAffected.ApplyStatusEffect(Effect.CloneStatusEffect(), Stacks);
    }

    public override string Name()
    {
        return GivenName;
    }

    public override string Description()
    {
        return GivenDescription;
    }
}

