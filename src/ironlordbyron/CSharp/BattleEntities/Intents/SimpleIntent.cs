using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Intents;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class SimpleIntent : AbstractIntent
{

    public SimpleIntent(AbstractBattleUnit source, ProtoGameSprite protoSprite) : base(source)
    {
        this.ProtoSprite = protoSprite;
    }

    public override string GetOverlayText()
    {
        return "";
    }


}


public static class IntentsFromPercentBase
{

    public static List<AbstractIntent> AttackRandomPc(
        AbstractBattleUnit source,
        int percentDamage, int numHits = 1)
    {
        int damagePerHit = GameState.Instance.DoomCounter.GetAdjustedDamage(percentDamage);
        return SingleUnitAttackIntent.AttackRandomPc(source, damagePerHit, numHits)
            .ToSingletonList<AbstractIntent>();
    }
    public static List<AbstractIntent> AttackRandomPcWithDebuff(
        AbstractStatusEffect debuff_requiresSetStacks,
        AbstractBattleUnit source,
        int percentDamage,
        int numHits = 1)
    {
        int damagePerHit = GameState.Instance.DoomCounter.GetAdjustedDamage(percentDamage);

        var attackIntent = SingleUnitAttackIntent.AttackRandomPc(source, damagePerHit, numHits);
        return new List<AbstractIntent>
        {
            attackIntent,
            DebuffOtherIntent.StatusEffect(source, attackIntent.Target, debuff_requiresSetStacks.CloneStatusEffect(), debuff_requiresSetStacks.Stacks)
        };
    }

    internal static List<AbstractIntent> DefendSelf(AbstractBattleUnit self, int blockAmount)
    {
        return new List<AbstractIntent>()
        {
            // todo;

        };
    }

    public static List<AbstractIntent> DoMagic(AbstractBattleUnit source,
        Action action)
    {
        return new List<AbstractIntent>()
        {

            // todo;
        };
    }


    public static List<AbstractIntent> AddCardsToDiscardPile(
        List<AbstractCard> cards,
        AbstractBattleUnit source,
        AbstractBattleUnit target = null)
    {
        return cards.Select(item => DebuffOtherIntent.AddCardToDiscardPile(source, null, item)).ToList<AbstractIntent>();
    }

    public static List<AbstractIntent> AttackRandomPcWithCardToDiscardPile(
        AbstractCard card,
        AbstractBattleUnit source,
        int percentDamage,
        int numHits = 1)
    {
        int damagePerHit = GameState.Instance.DoomCounter.GetAdjustedDamage(percentDamage);

        var attackIntent = SingleUnitAttackIntent.AttackRandomPc(source, damagePerHit, numHits);
        return new List<AbstractIntent>
        {
            attackIntent,
            DebuffOtherIntent.AddCardToDiscardPile(source, attackIntent.Target, card)
        };
    }

    public static List<AbstractIntent> AttackSetOfPcs(AbstractBattleUnit source,
        int percentDamage, int numHits, int numEnemies)
    {
        int damagePerHit = GameState.Instance.DoomCounter.GetAdjustedDamage(percentDamage);

        var enemiesToHit = GameState.Instance.AllyUnitsInBattle
            .Where(item => !item.IsDead)
            .PickRandom(numEnemies);
        return enemiesToHit
            .Select(target => new SingleUnitAttackIntent(source, target, damagePerHit, numHits))
            .ToList<AbstractIntent>();
    }

    public static List<AbstractIntent> AttackAllPcs(AbstractBattleUnit source,
        int percentDamage, int numHits)
    {
        int damagePerHit = GameState.Instance.DoomCounter.GetAdjustedDamage(percentDamage);
        return AttackSetOfPcs(source, damagePerHit, numHits, 20); //20 arbitrarily chosen; will hit everyone
    }

    public static List<AbstractIntent> BuffSelf(AbstractBattleUnit self,
        AbstractStatusEffect statusEffect,
        int stacks = 1)
    {
        return new BuffSelfIntent(self, statusEffect, stacks)
            .ToSingletonList<AbstractIntent>();
    }

    public static List<AbstractIntent> StatusEffectToRandomPc(
        AbstractBattleUnit source,
        AbstractStatusEffect effect,
        int stacks)
    {
        return DebuffOtherIntent.StatusEffectToRandomPc(
            source,
            effect, stacks
            ).ToSingletonList<AbstractIntent>();
    }

    public static List<AbstractIntent> BuffOther(AbstractBattleUnit self,
    AbstractStatusEffect statusEffect,
    int stacks = 1,
    Func<AbstractBattleUnit> battleUnitDecider = null)
    {
        var intent = new BuffOtherIntent(self, statusEffect, stacks);
        if (battleUnitDecider != null)
        {
            intent.BattleUnitDeciderFunction = battleUnitDecider;
        }
        return intent.ToSingletonList<AbstractIntent>();

    }

    public static List<AbstractIntent> Charging(AbstractBattleUnit unit)
    {
        return new List<AbstractIntent> { (new ChargingIntent(unit)) };
    }
}
public static class IntentRotation
{
    public static List<AbstractIntent> RandomIntent(params List<AbstractIntent>[] possibilities)
    {
        return possibilities.ToList().PickRandom();
    }

    public static List<AbstractIntent> FixedRotation(params List<AbstractIntent>[] possibilities)
    {
        var turnModNumOptions = GameState.Instance.BattleTurn % possibilities.Count();
        return possibilities.ToList()[turnModNumOptions];
    }

    public static List<AbstractIntent> LeadupAndRepeatLastOneForever(
        params List<AbstractIntent>[] leadup)
    {
        var turnOrLast = Math.Min(GameState.Instance.BattleTurn, leadup.Count() - 1);
        return leadup[turnOrLast];
    }

    public static List<AbstractIntent> LeadupAndThenGenerateIntentsFromFunction(
        Func<int, List<AbstractIntent>> generateLastAction,
        params List<AbstractIntent>[] leadup)
    {
        var turn = GameState.Instance.BattleTurn;

        if (turn <= leadup.Count() - 1)
        {
            return leadup[turn];
        }

        return generateLastAction(GameState.Instance.BattleTurn);
    }

}

public static class IntentsFromBaseDamage
{
    public static List<AbstractIntent> AttackRandomPc(
        AbstractBattleUnit source,
        int damagePerHit, int numHits = 1)
    {
        return SingleUnitAttackIntent.AttackRandomPc(source, damagePerHit, numHits)
            .ToSingletonList<AbstractIntent>();
    }

    public static List<AbstractIntent> AttackSetOfPcs(AbstractBattleUnit source,
        int damagePerHit, int numHits, int numEnemies)
    {
        var enemiesToHit = GameState.Instance.AllyUnitsInBattle
            .Where(item => !item.IsDead)
            .PickRandom(numEnemies);
        return enemiesToHit
            .Select(target => new SingleUnitAttackIntent(source, target, damagePerHit, numHits))
            .ToList<AbstractIntent>();
    }

    public static List<AbstractIntent> AttackAllPcs(AbstractBattleUnit source,
        int damagePerHit, int numHits)
    {
        return AttackSetOfPcs(source, damagePerHit, numHits, 20); //20 arbitrarily chosen; will hit everyone
    }

    public static List<AbstractIntent> BuffSelf(AbstractBattleUnit self,
        AbstractStatusEffect statusEffect,
        int stacks = 1)
    {
        return new BuffSelfIntent(self, statusEffect, stacks)
            .ToSingletonList<AbstractIntent>();
    }


    public static List<AbstractIntent> RandomIntent(params List<AbstractIntent>[] possibilities)
    {
        return possibilities.ToList().PickRandom();
    }

    public static List<AbstractIntent> FixedRotation(params List<AbstractIntent>[] possibilities)
    {
        var turnModNumOptions = GameState.Instance.BattleTurn % possibilities.Count();
        return possibilities.ToList()[turnModNumOptions];
    }

    public static List<AbstractIntent> LeadupAndRepeatLastOneForever(
        params List<AbstractIntent>[] leadup)
    {
        var turnOrLast = Math.Min(GameState.Instance.BattleTurn, leadup.Count() - 1);
        return leadup[turnOrLast];
    }

    public static List<AbstractIntent> LeadupAndThenGenerateIntentsFromFunction(
        Func<int, List<AbstractIntent>> generateLastAction,
        params List<AbstractIntent>[] leadup)
    {
        var turn = GameState.Instance.BattleTurn;

        if (turn <= leadup.Count() - 1)
        {
            return leadup[turn];
        }

        return generateLastAction(GameState.Instance.BattleTurn);
    }
    public static List<AbstractIntent> DefendSelf(AbstractBattleUnit self,
        int shieldValue)
    {
        return new DefendSelfIntent(self,
            shieldValue)
            .ToSingletonList<AbstractIntent>();
    }
    public static List<AbstractIntent> DebuffRandomOtherOnAttack(AbstractBattleUnit self, AbstractStatusEffect debuff, int stacks, int damage)
    {
        return new DebuffOtherIntent(self, () =>
        {
            var target = IntentTargeting.GetRandomLivingPlayerUnit();

            ActionManager.Instance.AttackUnitForDamage(target,
                self,
                damage,
                cardPlayed: null);

            ActionManager.Instance.ApplyStatusEffect(target,
                debuff,
                stacks);

        }).ToSingletonList<AbstractIntent>();
    }

    public static List<AbstractIntent> DebuffRandomOther(AbstractBattleUnit self, AbstractStatusEffect debuff_requiresStacksSet)
    {
        return new DebuffOtherIntent(self, () =>
        {
            var target = IntentTargeting.GetRandomLivingPlayerUnit();

            ActionManager.Instance.ApplyStatusEffect(target,
                debuff_requiresStacksSet,
                debuff_requiresStacksSet.Stacks);
        }).ToSingletonList<AbstractIntent>();
    }

}
