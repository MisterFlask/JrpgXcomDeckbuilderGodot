using System.Collections.Generic;

public class ArmoredEnemyUnit : AbstractEnemyUnit
{
    public ArmoredEnemyUnit()
    {
        this.CharacterFullName = "ArmoredEnemy";
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/RoboVAK", color: Color.blue);
        this.MaxHp = 30;
        this.ApplyStatusEffect(new ArmoredStatusEffect(), stacks: 4);
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        return IntentsFromBaseDamage.FixedRotation(
            IntentsFromBaseDamage.BuffSelf(this, new StrengthStatusEffect(), 5),
            IntentsFromBaseDamage.AttackRandomPc(this, 1, 1)
        );

    }



}
