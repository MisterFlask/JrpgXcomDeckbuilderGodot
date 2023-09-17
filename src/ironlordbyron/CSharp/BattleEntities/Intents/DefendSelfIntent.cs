using Godot;
using System;

public class DefendSelfIntent : AbstractIntent
{
    private int baseDefenseGranted;
    public DefendSelfIntent(AbstractBattleUnit source, int baseDefenseGranted) : base(source, source.ToSingletonList())
    {
        ProtoSprite = IntentIcons.DefendIntent;
        this.baseDefenseGranted = baseDefenseGranted;
    }

    public override string GetGenericDescription()
    {
        return "This unit will defend itself next turn.";
    }
    protected override IntentPrefab GeneratePrefab(Node2D parent)
    {
        throw new NotImplementedException();
    }

    protected override void Execute()
    {

    }

    public override string GetOverlayText()
    {
        return $"";
    }
}