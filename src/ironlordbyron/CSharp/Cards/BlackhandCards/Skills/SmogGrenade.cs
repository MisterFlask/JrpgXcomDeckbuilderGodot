using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards;

/// <summary>
/// Applies Weak to all enemies.
/// </summary>
public class SmogGrenade : AbstractCard
{
    public SmogGrenade()
    {
        SetCommonCardAttributes("Smog Grenade", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
        BaseDefenseValue = 3;
        ProtoSprite = ProtoGameSprite.BlackhandIcon("smog-grenade");

    }

    public override string DescriptionInner()
    {
        return $"Apply 1 Weak to all enemies; apply 3 defense to all allies. Exhaust.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        foreach (var character in state().EnemyUnitsInBattle)
        {
            action().ApplyStatusEffect(character, new WeakenedStatusEffect(), 1);
        }
        foreach (var ally in state().AllyUnitsInBattle)
        {
            action().ApplyDefense(ally, this.Owner, BaseDefenseValue);
        }
    }
}
