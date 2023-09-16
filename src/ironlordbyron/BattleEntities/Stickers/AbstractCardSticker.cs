using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;
using System;
using Assets.CodeAssets.UI.CardParts;

/// <summary>
/// These are used for card "passives"
/// </summary>
public abstract class AbstractCardSticker
{
    public AbstractCard CardAttachedTo { get; set; }
    public AbstractCard card => CardAttachedTo;
    public CardStickerPrefab Prefab { get; set; }
    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon();

    /// <summary>
    /// Added onto the end of the card description.
    /// </summary>
    public virtual string CardDescriptionAddendum()
    {
        return "";
    }

    public virtual string GetCardTooltipIfAny()
    {
        return null;
    }

    public virtual void OnAddedToCardInner(AbstractCard card)
    {
        // do stuff like change its attack damage or whatever
    }

    public void OnAddedToCard(AbstractCard card)
    {
        
    }

    public AbstractCardSticker CopySticker()
    {
        return this.MemberwiseClone() as AbstractCardSticker;
    }


    public virtual void OnThisCardPlayed(AbstractCard card, AbstractBattleUnit target)
    {

    }

    public virtual void OnCardDrawn(AbstractCard card)
    {

    }
    public virtual void EndOfBattlePassiveTrigger()
    {

    }
    public virtual void OnTurnStart(AbstractCard card)
    {

    }


    public virtual void OnManualDiscard(AbstractCard card)
    {

    }

    public virtual void OnEndOfTurnWhileInHand(AbstractCard card)
    {
        
    }

    public virtual bool MeetsAdditionalRequirementToBePlayed(AbstractCard card)
    {
        return true;
    }

    /// <summary>
    /// Return "false" if you want to avoid having this sticker attached to the card provided.
    /// </summary>
    public virtual bool IsCardTagApplicable(AbstractCard card)
    {
        return true;
    }

    public virtual CardVisualTag GetVisualTagIfAny()
    {
        return null;
    }
}

public class NascentCardSticker: AbstractCardSticker
{
    public override void OnManualDiscard(AbstractCard card)
    {
        CardAbilityProcs.ProcNascent(card);
    }
}

public class ExertCardSticker: AbstractCardSticker
{
    public override void OnThisCardPlayed(AbstractCard card, AbstractBattleUnit target)
    {
        CardAbilityProcs.ProcExert(card);
    }
}

public class GildedCardSticker: AbstractCardSticker
{
    public GildedCardSticker(int initialValue)
    {
        this.GildedValue = initialValue;
    }
    public override string CardDescriptionAddendum()
    {
        return $"Stash {GildedValue}";
    }

    public int GildedValue { get; set; }
    public override void EndOfBattlePassiveTrigger()
    {
        if (!this.CardAttachedTo.IsExhausted())
        {
            CardAbilityProcs.ChangeMoney(GildedValue);
        }
    }

    public override void OnThisCardPlayed(AbstractCard card, AbstractBattleUnit target)
    {
        if (card.Id == CardAttachedTo.Id)
        {
            GildedValue -= 2;
            if (GildedValue < 0)
            {
                GildedValue = 2;
            }
        }
    }
}

public class ExhaustCardSticker : AbstractCardSticker
{
    public override string CardDescriptionAddendum()
    {
        return "Exhaust.";
    }

    public override void OnThisCardPlayed(AbstractCard card, AbstractBattleUnit target)
    {
        card.Action_Exhaust();
    }
}

public class BasicAttackTargetSticker: AbstractCardSticker
{
    public override string CardDescriptionAddendum()
    {
        return $"Deal {card.DisplayedDamage()} to target.";
    }

    public override void OnThisCardPlayed(AbstractCard card, AbstractBattleUnit target)
    {
        ActionManager.Instance.AttackWithCard(card, target);
    }
}

public class BasicAttackRandomEnemyForSpecificDamageSticker : AbstractCardSticker
{
    public int Damage { get; set; } = 0;
    public override string CardDescriptionAddendum()
    {
        return $"Deal {card.DisplayedDamage(Damage)} to a random enemy.";
    }

    public override void OnThisCardPlayed(AbstractCard card, AbstractBattleUnit target)
    {
        ActionManager.Instance.AttackUnitForDamage(target, card.Owner, Damage, card);
    }
}

public class BasicAttackRandomEnemySticker: AbstractCardSticker
{
    public override string CardDescriptionAddendum()
    {
        return $"Deal {card.DisplayedDamage()} to a random enemy.";
    }

    public override void OnThisCardPlayed(AbstractCard card, AbstractBattleUnit target)
    {
        ActionManager.Instance.AttackWithCard(card, target);
    }
}

public class BasicDefendTargetSticker : AbstractCardSticker
{
    public override string CardDescriptionAddendum()
    {
        return $"Apply {card.DisplayedDefense()} block to target.";
    }

    public override void OnThisCardPlayed(AbstractCard card, AbstractBattleUnit target)
    {
        ActionManager.Instance.ApplyDefenseFromCard(card, target);
    }
}

public class BasicDefendSelfSticker : AbstractCardSticker
{
    public override string CardDescriptionAddendum()
    {
        return $"Apply {card.DisplayedDefense()} block to self.";
    }

    public override void OnThisCardPlayed(AbstractCard card, AbstractBattleUnit target)
    {
        ActionManager.Instance.ApplyDefenseFromCard(card, card.Owner);
    }
}
public class BasicApplyStatusEffectToTargetSticker : AbstractCardSticker
{

    public BasicApplyStatusEffectToTargetSticker(AbstractStatusEffect effect, int stacks)
    {
        Effect = effect;
        Stacks = stacks;
    }

    public AbstractStatusEffect Effect { get; }
    public int Stacks { get; }

    public override string CardDescriptionAddendum()
    {
        return $"Apply {Stacks} {Effect.Name} to target.";
    }

    public override void OnThisCardPlayed(AbstractCard card, AbstractBattleUnit target)
    {
        ActionManager.Instance.ApplyStatusEffect(target, Effect, Stacks);
    }
}

public class BasicApplyStatusEffectToSelfSticker : AbstractCardSticker
{

    public BasicApplyStatusEffectToSelfSticker(AbstractStatusEffect effect, int stacks)
    {
        Effect = effect;
        Stacks = stacks;
    }

    public AbstractStatusEffect Effect { get; }
    public int Stacks { get; }

    public override string CardDescriptionAddendum()
    {
        return $"Apply {Stacks} {Effect.Name} to self.";
    }

    public override void OnThisCardPlayed(AbstractCard card, AbstractBattleUnit target)
    {
        ActionManager.Instance.ApplyStatusEffect(card.Owner, Effect, Stacks);
    }
}