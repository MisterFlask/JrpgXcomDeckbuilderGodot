public class NoxiousGasesMissionModifier : MissionModifier
{
    public override string Description()
    {
        return "At the beginning of combat, a random 4 cards in your deck gain the Noxious sticker.";
    }

    public override void OnMissionCombatBegins()
    {
        var fourRandomCardsInDeck = GameState.Instance.Deck.TotalDeckList.Shuffle().TakeUpTo(4);
        foreach (var card in fourRandomCardsInDeck)
        {
            ActionManager.Instance.AddStickerToCard(card, new NoxiousCardSticker());
        }
    }

    public override int IncrementalMoney()
    {
        return 25;
    }
}

public class NoxiousCardSticker : AbstractCardSticker
{
    public override string CardDescriptionAddendum()
    {
        return "Noxious: When played, take 3 stress and 3 damage.";
    }

    public override void OnThisCardPlayed(AbstractCard card, AbstractBattleUnit target)
    {
        if (card.Owner != null) return;

        ActionManager.Instance.DamageUnitNonAttack(card.Owner, null, 3);
        ActionManager.Instance.ApplyStress(card.Owner, 3);
    }
}