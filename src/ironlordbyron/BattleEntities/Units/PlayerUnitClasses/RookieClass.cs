using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.RookieCards;

public class RookieClass : AbstractSoldierClass
{

    public RookieClass()
    {
        EmblemIcon = ProtoGameSprite.EmblemIcon("dread-emblem");
        PortraitFolder = "BlackhandPortraits"; // no longer interested in using this class
    }
    public override List<AbstractCard> StartingCards()
    {
        return new List<AbstractCard>
        {
            new RookieDefend(),
            new RookieAttack(),
            new Grenade(),
            new CoveringFire()
        };
    }

    public override void LevelUpEffects(AbstractBattleUnit me)
    {
        base.LevelUpEffects(me);
        me.RemoveCardsFromPersistentDeck(me.CardsInPersistentDeck);//Remove all cards
        me.ChangeClass(GetRandomNewClass());

        var newClass = me.SoldierClass;
        me.AddCardsToPersistentDeck(newClass.StartingCards());
        var commonCardToAdd = newClass.UniqueCardRewardPool()
            .Where(item => item.Rarity == Rarity.COMMON).PickRandom();
        if (commonCardToAdd == null)
        {
            Log.Error("No common cards in pool for class " + newClass.Name());
        }
        me.AddCardsToPersistentDeck(new List<AbstractCard> {
            commonCardToAdd.CopyCard(),
            commonCardToAdd.CopyCard()
        });
        Log.Info("Added common cards to deck on promotion: 2 copies of " + commonCardToAdd.Name);
    }

    private AbstractSoldierClass GetRandomNewClass()
    {
        return GetClassesEligibleForPromotion().PickRandom();
    }

    public static List<AbstractSoldierClass> GetClassesEligibleForPromotion()
    {
        return new List<AbstractSoldierClass>
        {
            new HammerSoldierClass(),
            new DiabolistSoldierClass(),
            new CogSoldierClass(),
            new BlackhandSoldierClass(),
            new ArchonSoldierClass()
        };// todo
    }

    public override string Name()
    {
        return "Rookie";
    }
}
