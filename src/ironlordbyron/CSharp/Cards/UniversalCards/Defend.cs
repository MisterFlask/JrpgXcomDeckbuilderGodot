using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards;

public class Defend : AbstractCard
{
    public Defend()
    {
        this.BaseDefenseValue = 5;
        this.SetCommonCardAttributes("Defend", Rarity.BASIC, TargetType.ALLY, CardType.SkillCard, 1, protoGameSprite: ProtoGameSprite.FromGameIcon("Sprites/shield-reflect"));

    }


    public override string DescriptionInner()
    {
        return $"Applies {DisplayedDefense()} defense to target ally.";
    }
    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().ApplyDefense(target, this.Owner, this.BaseDefenseValue);
    }
}
