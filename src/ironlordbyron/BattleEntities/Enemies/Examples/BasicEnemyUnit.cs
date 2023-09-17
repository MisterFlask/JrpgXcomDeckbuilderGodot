using System.Collections;
using System.Collections.Generic;

public class BasicEnemyUnit : AbstractEnemyUnit
{
    public BasicEnemyUnit()
    {
        this.MaxHp = 20;
        this.CurrentHp = 20;
        this.CharacterFullName = "Basic Enemy";
        this.IsAlly = false;
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(color: Color.red);
        this.IsAiControlled = true;
        this.StatusEffects.Add(new DyingStatusEffect());
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        return IntentsFromBaseDamage.AttackRandomPc(this, 5);
    }
}
