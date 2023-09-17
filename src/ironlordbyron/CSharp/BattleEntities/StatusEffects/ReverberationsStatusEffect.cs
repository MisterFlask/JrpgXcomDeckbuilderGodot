using System;

public class Reverberations : AbstractStatusEffect
{
    public Reverberations()
    {
        this.Name = "Reverberations";
        // Assuming ProtoGameSprite is a class that helps in loading sprite. Replace "your_sprite_path" with the actual path.
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("abstract-024");
    }

    // This method will be triggered whenever a card is played
    public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool cardIsOwnedByMe)
    {
        AbstractBattleUnit cardOwner = cardPlayed.Owner; // Will use the correct attribute once confirmed
        if (cardOwner != null)
        {
            // Assuming there is a CurrentStress property to get the current stress level of the unit
            int stressToApply = Math.Min(Stacks, 90 - cardOwner.CurrentStress);
            ActionManager.Instance.ApplyStress(cardOwner, stressToApply);
        }
    }

    public override string Description => $"Cards played stress their owner by {Stacks}, to a max of 90.";
}
