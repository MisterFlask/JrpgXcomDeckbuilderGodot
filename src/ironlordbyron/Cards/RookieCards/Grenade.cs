using System.Collections;
using Assets.CodeAssets.Cards;

public class Grenade : AbstractCard
{
    public Grenade()
    {
        ProtoSprite = ProtoGameSprite.RookieIcon("grenade");
        SoldierClassCardPools.Add(typeof(RookieClass));
        this.BaseDamage = 5;
        TargetType = TargetType.ENEMY;
        CardType = CardType.AttackCard;
        Name = "Grenade";
    }

    public override string DescriptionInner()
    {
        return $"Deals {displayedDamage()} damage to the target, then {displayedDamage()} damage to all enemies.  Expend.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage, this);

        foreach(var otherTarget in enemies())
        {
            action().AttackUnitForDamage(otherTarget, this.Owner, BaseDamage, this);
        }
        action().ExhaustCard(this);
    }
}
