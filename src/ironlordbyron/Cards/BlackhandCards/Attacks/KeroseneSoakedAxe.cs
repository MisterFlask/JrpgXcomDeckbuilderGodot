using System.Collections;
using Assets.CodeAssets.Cards;

public class KeroseneSoakedAxe : AbstractCard
{
    public KeroseneSoakedAxe()
    {
        SetCommonCardAttributes("Kerosene-Soaked Axe", Rarity.UNCOMMON, TargetType.ENEMY, CardType.AttackCard, 2);
        this.DamageModifiers.Add(new SlayerDamageModifier());
        BaseDamage = 14;
        ProtoSprite = ProtoGameSprite.BlackhandIcon("fire-axe");

    }
    public override string DescriptionInner()
    {
        return $"Deal {DisplayedDamage()} damage to an enemy.  Apply 10 Fumes.  Slayer.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage, this);
        action().ApplyStatusEffect(target, new FumesStatusEffect(), 10);
    }
}
