using Godot;
using System.Collections.Generic;

/**
 *   30 HP.   Attacks:  2x3.
 *   TODO: a Flower debuff that goes on cards in your deck which causes them to spawn a Greywing when played.
 *   That would be pretty sweet.
 *   (Though I have not yet created card-specific effects.)
 */
public class Greywing : AbstractEnemyUnit
{
    public Greywing()
    {
        this.CharacterFullName = "Greywing";
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/BlackBirdi", color: Colors.White);
        this.MaxHp = 30;
        this.ApplyStatusEffect(new GreywingWoundOnDeath(), stacks: 4);
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        return new List<AbstractIntent>
        {
            new BuffSelfIntent(this, new StrengthStatusEffect()),
            SingleUnitAttackIntent.AttackRandomPc(this, 1, 2)
        }
        .PickRandom()
        .ToSingletonList();
    }
}


/// <summary>
///  When he dies, he applies four Wounded to the unit attacking.
/// </summary>
public class GreywingWoundOnDeath : AbstractStatusEffect
{
    public GreywingWoundOnDeath()
    {
        Name = "Greywing's Revenge";
        ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/falling-bang", Colors.Yellow);
    }
    public override string Description => $"This applies Burning to whoever killed it equal to number of stacks.";

    public override void OnDeath(AbstractBattleUnit unitThatKilledMe, AbstractCard cardPlayedIfAny)
    {
        if (unitThatKilledMe != null)
        {
            unitThatKilledMe.ApplyStatusEffect(new BurningStatusEffect(), stacks: Stacks);
        }
    }
}
