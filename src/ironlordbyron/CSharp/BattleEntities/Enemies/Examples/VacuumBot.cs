using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Examples
{

    /// <summary>
    /// Each turn, sucks up a card from your draw or discard piles
    /// if you hit it three times in a turn, spits the card back out
    /// if the card stays in for two turn starts, it's exhausted.
    /// </summary>
    public class VacuumBot : AbstractEnemyUnit
    {
        public override List<AbstractIntent> GetNextIntents()
        {
            return SingleUnitAttackIntent.AttackRandomPc(this, 20, 1)
                .ToSingletonList<AbstractIntent>();
        }
    }
}


public class VacuumBotAbility : AbstractStatusEffect
{
    public override void OnTurnEnd()
    {
        var drawPile = GameState.Instance.Deck.DrawPile;
        var discardPile = GameState.Instance.Deck.DiscardPile;
        var total = new List<AbstractCard>();
        total.AddRange(drawPile);
        total.AddRange(discardPile);
        var randomCard = total.PickRandom();
        action().ExhaustCard(randomCard);
    }

    public override string Description => "Each turn after drawing, exhausts a card from either your draw or discard piles.";
}