using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractIntent
{

    public static IntentIcons IntentIcons = new IntentIcons();

    public AbstractIntent(AbstractBattleUnit source,
        List<AbstractBattleUnit> unitsTargeted = null,
        ProtoGameSprite protoSprite = null)
    {
        this.ProtoSprite = protoSprite ?? ImageUtils.ProtoGameSpriteFromGameIcon(color: Colors.Blue);
        this.Source = source;
        this.UnitsTargeted = unitsTargeted ?? new List<AbstractBattleUnit>();
    }

    public string Id = Guid.NewGuid().ToString();

    public abstract string GetGenericDescription();

    public void ExecuteIntent()

    {
        if (Source.IsDead)
        {
            return;
        }
        Execute();
    }

    protected abstract void Execute();

    protected virtual bool CurrentlyAvailableForUsage()
    {
        return true;
    }

    protected virtual IntentPrefab GeneratePrefab(Node2D parent)
    {
        throw new Exception();
    }

    public IntentPrefab GeneratePrefabAndAssign(Node2D parent)
    {
        throw new Exception();
    }

    // can be an empty list
    public List<AbstractBattleUnit> UnitsTargeted { get; set; } = new List<AbstractBattleUnit>();

    public AbstractBattleUnit Source { get; set; }

    public ProtoGameSprite ProtoSprite { get; set; }

    public static AbstractIntent GetIntentFromShuffle(List<AbstractIntent> options)
    {
        return options.Shuffle().First();
    }

    public static AbstractIntent GetIntentFromOrderedActions(List<AbstractIntent> optionsInOrder, int turnNumber)
    {
        var index = turnNumber % optionsInOrder.Count;
        return optionsInOrder[index];
    }

    public abstract string GetOverlayText();
}

public class IntentIcons
{
    public ProtoGameSprite AttackIntent = ProtoGameSprite.FromGameIcon("Sprites/IntentIcons/knife-thrust");
    public ProtoGameSprite DefendIntent = ProtoGameSprite.FromGameIcon("Sprites/IntentIcons/round-shield");
    public ProtoGameSprite UnknownIntent = ProtoGameSprite.FromGameIcon("Sprites/IntentIcons/uncertainty");
    public ProtoGameSprite MagicIntent = ProtoGameSprite.FromGameIcon("Sprites/IntentIcons/magick-trick");
    public ProtoGameSprite DebuffIntent = ProtoGameSprite.FromGameIcon("Sprites/IntentIcons/poison-bottle-2");
    public ProtoGameSprite BuffIntent = ProtoGameSprite.FromGameIcon("Sprites/IntentIcons/unstable-orb");
    public ProtoGameSprite DazedIntent = ProtoGameSprite.FromGameIcon("Sprites/IntentIcons/star-swirl");


}
