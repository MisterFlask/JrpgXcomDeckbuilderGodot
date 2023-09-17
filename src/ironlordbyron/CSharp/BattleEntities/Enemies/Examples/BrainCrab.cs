using Godot;
using System.Collections.Generic;

public class BrainCrab : AbstractEnemyUnit
{
    public BrainCrab()
    {
        this.CharacterFullName = "Brain Crab";
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/RoboVAK", color: Colors.White);
        this.MaxHp = 14;
        this.ApplyStatusEffect(new AddsParasiteOnDealingDamage(), stacks: 1);
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        return new List<AbstractIntent>
        {
            new BuffSelfIntent(this, new StrengthStatusEffect(), 5),
            SingleUnitAttackIntent.AttackRandomPc(this, 1, 1)
        }
        .PickRandom()
        .ToSingletonList();
    }

}
public class AddsParasiteOnDealingDamage : AbstractStatusEffect
{
    //todo

    public AddsParasiteOnDealingDamage()
    {
        Name = "Parasitoid";
        ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/falling-bang", Colors.Yellow);
    }


    public override string Description => "Whenever this deals unblocked damage, adds a Parasite to your discard pile";
}
