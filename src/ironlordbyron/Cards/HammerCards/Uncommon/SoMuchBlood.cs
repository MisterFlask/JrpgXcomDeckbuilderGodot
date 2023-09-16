using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards.Uncommon
{
    public class SoMuchBlood : AbstractCard
    {
        // Apply 5 block, plus another 1 for each 2 of target's missing HP.
        
        public SoMuchBlood()
        {
            SetCommonCardAttributes("So Much Blood", Rarity.UNCOMMON, TargetType.ALLY, CardType.SkillCard, 1);
            BaseDefenseValue = 5;
            ProtoSprite = ProtoGameSprite.HammerIcon("spatter");

        }

        public override string DescriptionInner()
        {
            return $"Apply 5 block, plus another 1 for each 2 of target's missing HP.  Increase this card's cost by 1.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyDefenseToTarget(target, BaseDefenseValue + (target.MaxHp - target.CurrentHp) / 2);
            StaticBaseEnergyCost += 1;
        }
    }
}