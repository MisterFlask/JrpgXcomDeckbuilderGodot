using System.Collections;
using System.Linq;
using Assets.CodeAssets.Cards;

public class Bayonet : AbstractCard
{
    public Bayonet()
    {
        SoldierClassCardPools.Add(typeof(RookieClass));
        Name = "Bayonet";
        BaseDamage = 10;
        TargetType = TargetType.ENEMY;
        CardType = CardType.AttackCard;
        this.ProtoSprite = GameIconProtoSprite.FromGameIcon(path: "Sprites/bayonet");

    }

    public override int BaseEnergyCost()
    {
        if (Owner == null)
        {
            return 2;
        }

        if (Owner.HasStatusEffect<AdvancedStatusEffect>())
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        Require.NotNull(target);
        action().AttackUnitForDamage(target, Owner, BaseDamage, this);
        var cardDiscarded = action().PromptDiscardOfSingleCard();
        action().PushActionToBack("Bayonet_OnPlay", () =>
        {
            Debug.Log("Cards discarded: " + cardDiscarded.CardsSelected.GetCardNames());
        });
    }

    public override string DescriptionInner()
    {
        return $"Deals {displayedDamage()} damage to an enemy unit.  Costs 1 less if Advanced.  Discard a card.";
    }
}
