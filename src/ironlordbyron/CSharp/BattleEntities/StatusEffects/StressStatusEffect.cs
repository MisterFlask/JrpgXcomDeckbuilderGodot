using System.Collections.Generic;

public class StressStatusEffect : AbstractStatusEffect
{

    public StressStatusEffect()
    {
        Name = "Stress";
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("shattered-heart");
    }
    public override string Description => $"If this goes above 100," +
        $"stress goes to 0, the character gets a permanent Madness card to their " +
        $"deck and the Snapped status effect (increases cost of all cards by 1.)";

    public override void OnApplicationOrIncrease()
    {
        if (Stacks > 100)
        {
            if (OwnerUnit.HasStatusEffect<SnappedStatusEffect>())
            {
                OwnerUnit.CurrentHp = 0;
            }
            else
            {
                Stacks = 0;
                action().ApplyStatusEffect(OwnerUnit, new SnappedStatusEffect(), 1);
                // todo: Madness card to character deck.
                action().AddCardToPersistentDeck(GetMadnessCard(), OwnerUnit);
            }
        }
    }

    /// <summary>
    /// todo: mechanic where if a character has a madness card, all subsequent ones are the same type.
    /// </summary>
    /// <returns></returns>
    public AbstractCard GetMadnessCard()
    {
        return new List<AbstractCard>
        {
            new Abusive()
        }.PickRandom();
    }
}
