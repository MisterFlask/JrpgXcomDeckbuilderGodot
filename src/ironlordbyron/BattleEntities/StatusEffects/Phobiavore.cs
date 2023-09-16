
public class Phobiavore : AbstractStatusEffect
{
    public Phobiavore()
    {
        this.Name = "Phobiavore";
        // Replace "your_sprite_path" with the actual path to the sprite.
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("abstract-085");
    }

    public override int DamageDealtAddition()
    {
        // Assuming there is a CurrentStress property to get the current stress level of the unit
        if (this.OwnerUnit.CurrentStress > 70)
        {
            return Stacks;
        }
        return 0;
    }

    public override string Description => $"Deals {Stacks} more damage to characters with >70 stress.";
}
