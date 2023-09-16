
public class LookUponMeAndDespair : AbstractStatusEffect
{
    public LookUponMeAndDespair()
    {
        this.Name = "Look Upon Me And Despair";
        // Replace "your_sprite_path" with the actual path to the sprite.
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("abstract-025");
    }

    // This method will be triggered whenever a card targets this unit
    public override void OnTargetedByCard(AbstractCard sourceCard)
    {
        if (sourceCard.Owner != null)
        {
            // Apply vulnerable stacks to the character that played the card
            // Assuming VulnerableStatusEffect is the class representing the vulnerable status effect
            ActionManager.Instance.ApplyStatusEffect(sourceCard.Owner, new VulnerableStatusEffect { Stacks = this.Stacks });
        }
    }

    public override string Description => $"Whenever a character targets this enemy with a card, that character gains {DisplayedStacks()} Vulnerable.";
}
