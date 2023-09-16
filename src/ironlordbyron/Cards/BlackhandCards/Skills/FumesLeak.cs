using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class FumesLeak : AbstractCard
{
    public FumesLeak()
    {
        SetCommonCardAttributes("Fumes Leak", Rarity.COMMON, TargetType.ENEMY, CardType.SkillCard, 1);
        BaseDefenseValue = 4;
        ProtoSprite = ProtoGameSprite.BlackhandIcon("bottle-vapors");
    }

    public override string DescriptionInner()
    {
        return $"Adds 8 Fumes to targeted enemy.  Apply {BaseDefenseValue} defense and 2 Stress to all allies.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().ApplyStatusEffect(target, new FumesStatusEffect(), 8);
        foreach(var ally in state().AllyUnitsInBattle)
        {
            action().ApplyDefense(ally, Owner, BaseDefenseValue);
            action().ApplyStatusEffect(ally, new StressStatusEffect(), 2);
        }
    }
}
