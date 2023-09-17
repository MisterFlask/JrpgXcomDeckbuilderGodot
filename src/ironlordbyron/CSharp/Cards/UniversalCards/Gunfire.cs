using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards;

public class Gunfire : AbstractCard
{
    public Gunfire()
    {
        BaseDamage = 6;
        this.SetCommonCardAttributes("Gunfire", Rarity.BASIC, TargetType.ENEMY, CardType.AttackCard, 1, protoGameSprite: ProtoGameSprite.FromGameIcon("Sprites/sword-wound"));
    }

    public override string DescriptionInner()
    {
        return $"Deal {DisplayedDamage()} damage.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage, this);
    }
}
