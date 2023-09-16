using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class IneptShot : AbstractCard
{
    public IneptShot()
    {
        ProtoSprite = ProtoGameSprite.RookieIcon("police-target");
        SoldierClassCardPools.Add(typeof(RookieClass));
        SetCommonCardAttributes("Inept Shot", Rarity.BASIC, TargetType.ENEMY, CardType.AttackCard, baseEnergyCost: 1, protoGameSprite: ProtoGameSprite.FromGameIcon("Sprites/bowman-sad"));
        BaseDamage = 4;
    }

    public override int BaseEnergyCost()
    {
        return 1;
    }

    public override string DescriptionInner()
    {
        return $"Deal {DisplayedDamage()} damage";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage, this);
    }
}
