using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class DebuffOtherIntent : SimpleIntent
{
    public Action OnPerformance { get; }

    public DebuffOtherIntent(AbstractBattleUnit owner, Action onPerformance): base(owner, 
        IntentIcons.DebuffIntent)
    {
        OnPerformance = onPerformance;
    }

    public static DebuffOtherIntent SomeAction(AbstractBattleUnit owner,
        Action onPerformance)
    {
        return new DebuffOtherIntent(owner, onPerformance);
    }

    public static DebuffOtherIntent StatusEffectToRandomPc(
        AbstractBattleUnit source,
        AbstractStatusEffect effect,
        int stacks)
    {
        return StatusEffect(source,
            GameState.Instance.AllyUnitsInBattle.Where(item => !item.IsDead).PickRandom(),
            effect, 
            stacks);

    }
    public static DebuffOtherIntent StatusEffectToAllPcs(
        AbstractBattleUnit source,
        AbstractStatusEffect effect,
        int stacks,
        AbstractStatusEffect effect2 = null,
        int stacks2 = 1)
    {
        return new DebuffOtherIntent(source, () =>
        {
            foreach(var character in GameState.Instance.AllyUnitsInBattle)
            {
                ActionManager.Instance.ApplyStatusEffect(character, effect, stacks);

                if (effect2 != null)
                {
                    ActionManager.Instance.ApplyStatusEffect(character, effect, stacks2);
                }
            }
        });

    }

    public static DebuffOtherIntent StatusEffect(
        AbstractBattleUnit source,
        AbstractBattleUnit target, 
        AbstractStatusEffect effect,
        int stacks = 1,
        AbstractStatusEffect effect2 = null,
        int stacks2 = 1)
    {
        return new DebuffOtherIntent(source, () =>
        {
            ActionManager.Instance.ApplyStatusEffect(target, effect, stacks);

            if (effect2 != null) {
                ActionManager.Instance.ApplyStatusEffect(target, effect, stacks2);
            }
        });
    }

    public static DebuffOtherIntent AddCardToDiscardPile(
        AbstractBattleUnit source,
        AbstractBattleUnit target,
        AbstractCard card)
    {
        return new DebuffOtherIntent(source, () =>
        {
            var cardCopy = card.CopyCard();
            cardCopy.Owner = target;
            ActionManager.Instance.CreateCardToBattleDeckDiscardPile(cardCopy);
        });
    }

    public override string GetGenericDescription()
    {
        return "This enemy is about to apply a debuff to one or more of your soldiers.";
    }

    public override string GetOverlayText()
    {
        return ""; // no overlay text for this class of intent.
    }

    protected override void Execute()
    {
        OnPerformance();
    }
}
