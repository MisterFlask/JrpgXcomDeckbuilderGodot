using System.Collections;
using Assets.CodeAssets.Cards;
using Assets.CodeAssets.GameLogic;

public class Flamer : AbstractCard
{
    public Flamer()
    {
        ProtoSprite = ProtoGameSprite.BlackhandIcon("flame");
        this.SoldierClassCardPools.Add(typeof(Rarity));
        SetCommonCardAttributes("Flamer", Rarity.BASIC, TargetType.ENEMY, CardType.AttackCard, 0);
        this.MagicNumber = 3;

    }

    public override string DescriptionInner()
    {
        return $"Applies {MagicNumber} Burning.  Increase the cost of this card by 1 and its Burning value by 2.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        ActionManager.Instance.ApplyStatusEffect(target, new BurningStatusEffect(), MagicNumber);
        this.PersistentCostModifiers.Add(new RestOfCombatCostModifier(1));
        this.MagicNumber += 2;
    }

}
