using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class Firewall : AbstractCard
{
    int TemporaryThornsGranted = 7;

    public Firewall()
    {

        BaseDefenseValue = 10;
        SetCommonCardAttributes("Firewall", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 2);
        ProtoSprite = ProtoGameSprite.BlackhandIcon("stone-wall");
    }

    public override string DescriptionInner()
    {
        return $"Apply {DisplayedDefense()} to ally.  Grant that ally {TemporaryThornsGranted} Temporary Thorns.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().ApplyDefense(target, Owner, BaseDefenseValue);
        action().ApplyStatusEffect(target, new TemporaryThorns(), TemporaryThornsGranted);
    }
}
