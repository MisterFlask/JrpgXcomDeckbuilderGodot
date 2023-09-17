using System.Collections.Generic;

public class TormentNexus : AbstractBattleUnit
{
    public TormentNexus()
    {
        this.MaxHp = 120;
        this.CurrentHp = 120; // Starting with max HP
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Boss Zero Machina");

        this.StatusEffects = new List<AbstractStatusEffect>
        {
            new Reverberations
            {
                Stacks = 1
            },
            new LookUponMeAndDespair()
            {
                Stacks = 1
            },
            new Phobiavore
            {
                Stacks = 5
            }
        };
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        // Setting up a fixed rotation of intents using the specified base damage values for each action
        return IntentRotation.FixedRotation(
             // Defend intent with a base defense value (replace with the correct value)
             IntentsFromBaseDamage.DefendSelf(this, 30),

             // Intent to add 5x Wounds to the discard pile (need guidance on how to implement this)

             // 40 damage intent
             IntentsFromBaseDamage.AttackRandomPc(this, 40, 1),

             // 3x5 damage intent
             IntentsFromBaseDamage.AttackRandomPc(this, 5, 3)
        );
    }

    // Other functionalities will go here
}
