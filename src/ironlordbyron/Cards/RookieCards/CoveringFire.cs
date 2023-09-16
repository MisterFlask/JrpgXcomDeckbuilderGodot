using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class CoveringFire : AbstractCard
{

    public CoveringFire()
    {
        ProtoSprite = ProtoGameSprite.RookieIcon("machine-gun");
        SoldierClassCardPools.Add(typeof(RookieClass));
        this.BaseDamage = 4;
        TargetType = TargetType.ENEMY;
        CardType = CardType.AttackCard;
        Name = "Covering Fire";
    }

    public override int BaseEnergyCost()
    {
        return 1;
    }

    public override string DescriptionInner()
    {
        return $"Deals {displayedDamage()} damage to the target.  Apply 1 Weak.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage, this);
        action().ApplyStatusEffect(target, new WeakenedStatusEffect());
    }
}
