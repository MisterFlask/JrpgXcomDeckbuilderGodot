using Godot;
using System.Collections.Generic;

public class BasicAllyUnit : AbstractBattleUnit
{
    public BasicAllyUnit()
    {
        this.MaxHp = 20;
        this.CurrentHp = 20;
        this.CharacterFullName = "Basic Ally";
        this.IsAlly = true;
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(color: Colors.Blue);
        this.IsAiControlled = false;
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        return new List<AbstractIntent>();
    }
}
