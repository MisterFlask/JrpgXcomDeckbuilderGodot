using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class IndiscriminateFire : AbstractCard
{
    public IndiscriminateFire()
    {
        BaseDamage = 5;
        SetCommonCardAttributes(
            "Friendly Fire", 
            Rarity.UNCOMMON, 
            TargetType.NO_TARGET_OR_SELF, 
            CardType.AttackCard,
            1);
        DamageModifiers.Add(new SweeperDamageModifier());
        ProtoSprite = ProtoGameSprite.BlackhandIcon("baby-face");

    }

    public override string DescriptionInner()
    {
        return $"Deal {DisplayedDamage()} damage and apply 4 Burning to ALL enemies; apply 2 Burning to ALL allies.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        foreach(var ally in state().AllyUnitsInBattle)
        {
            action().ApplyStatusEffect(ally, new BurningStatusEffect(), 2);
        }
        foreach(var enemy in state().EnemyUnitsInBattle)
        {
            action().ApplyStatusEffect(enemy, new BurningStatusEffect(), 4);
            action().AttackUnitForDamage(enemy, Owner, BaseDamage, this);
        }
    }
}
