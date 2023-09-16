using Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities;
using Assets.CodeAssets.Cards;
using System.Collections.Generic;
using UnityEngine;

public class Migo : AbstractBattleUnit
{
    public Migo()
    {
        CharacterNicknameOrEnemyName = "Mi-Go";
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Eldritch Corruption Treant");
        MaxHp = 100; // Set the appropriate HP value here

        // Adding the status effects for Mi-Go
        this.StatusEffects.Add(new Growing { Stacks = 2 });
        this.StatusEffects.Add(new FragileBranches{ Stacks = 2 });
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        // Randomly decide between attack and defend intents
        return IntentRotation.FixedRotation(
             IntentsFromBaseDamage.AttackRandomPc(this, 10, 1),
             IntentsFromBaseDamage.DefendSelf(this, 20));
    }
}
public class FragileBranches : AbstractStatusEffect
{
    public FragileBranches()
    {
        this.Name = "Fragile Branches";
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("dead-wood");
    }

    public override string Description => $"Loses {Stacks} strength when struck, to a minimum of 0.  When struck, shuffle a Fruiting Body into your draw pile.";

    public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
    {
        var strengthEffect = this.OwnerUnit.GetStatusEffect<StrengthStatusEffect>();
        if (strengthEffect != null)
        {
            strengthEffect.Stacks = Mathf.Max(0, strengthEffect.Stacks - Stacks);
        }

        var fruitingBodyCard = new FruitingBody();
        ActionManager.Instance.CreateCardToBattleDeckDrawPile(fruitingBodyCard, CardCreationLocation.SHUFFLE, owner: GameState.Instance.AllyUnitsInBattle.PickRandom());

    }
}
public class FruitingBody : AbstractCard
{
    public FruitingBody()
    {
        Name = "Fruiting Body";
        CardType = CardType.ErosionCard;
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("fruiting");

    }

    public override int BaseEnergyCost()
    {
        return 0;
    }

    public override string DescriptionInner()
    {
        return $"Draw a card.  {ownerDisplayString()} gets 3 stress.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
         
        ActionManager.Instance.DrawCards(1);
        ActionManager.Instance.ApplyStress(Owner, 3);
    }
}

