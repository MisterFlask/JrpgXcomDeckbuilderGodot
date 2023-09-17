using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using System;
using System.Collections.Generic;

public class Soldier : AbstractAllyUnit
{
    public static Soldier NULL_SOLDIER = new Soldier();

    public Soldier(AbstractSoldierClass soldierClass = null)
    {
        this.MaxHp = 10;
        this.MaxFatigue = 4;
        this.SoldierClass = soldierClass ?? new BlackhandSoldierClass();

        this.StartingCardsInDeck.AddRange(SoldierClass.StartingCards());
        this.ProtoSprite = GetRandomSoldierProtoSpriteForClass();
    }

    private ProtoGameSprite GetRandomSoldierProtoSpriteForClass()
    {
        if (SoldierClass?.PortraitFolder != null)
        {
            return GetPortraitForFolder("Portraits/" + SoldierClass.PortraitFolder);
        }

        return new GameIconProtoSprite();//default placeholder image
    }

    /// <summary>
    /// Returns a random portrait sprite for this soldier.
    /// </summary>
    /// <returns></returns>
    public ProtoGameSprite GetPortraitForFolder(string resourceFolderPath)
    {
        //todo
        throw new NotImplementedException();
    }

    public static AbstractBattleUnit GenerateFreshRookie()
    {
        var rookie = new Soldier().CloneUnit();

        return rookie;
    }

    public static AbstractBattleUnit GenerateSoldierOfClass(AbstractSoldierClass soldierClass,
        int level = 1)
    {
        var soldier = new Soldier(soldierClass).CloneUnit();
        for (int i = 1; i < level; i++)
        {
            soldier.LevelUp();
        }
        return soldier;
    }

    public override List<AbstractCard> CardsSelectableOnLevelUp()
    {
        // return list of cards
        return SoldierClass.GetCardRewardsForLevel(this.CurrentLevel, 3);
    }
}
