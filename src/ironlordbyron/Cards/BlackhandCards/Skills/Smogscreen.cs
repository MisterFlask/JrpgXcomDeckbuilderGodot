using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

// increases defense, increases stress
public class Smokescreen : AbstractCard
{
    public Smokescreen()
    {
        SetCommonCardAttributes("Smokescreen", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
        this.BaseDefenseValue = 8;
        ProtoSprite = ProtoGameSprite.BlackhandIcon("tv");

    }

    public override string DescriptionInner()
    {
        return $"Apply 1 Evade to all allies.  All allies gain 3 Stress.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        foreach (var character in state().AllyUnitsInBattle)
        {
            action().ApplyDefense(character, Owner, BaseDefenseValue);
            action().ApplyStatusEffect(target, new StressStatusEffect(), 3);
        }
    }
}
