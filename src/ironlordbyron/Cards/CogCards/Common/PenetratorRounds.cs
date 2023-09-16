using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Common
{
    public class PenetratorRounds : AbstractCard
    {
        public PenetratorRounds()
        {
            SetCommonCardAttributes("Penetrator Rounds", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);
            ProtoSprite = ProtoGameSprite.CogIcon("pierced-body");
        }

        // The leftmost attack in your hand gains "Whenever this deals damage to an enemy, apply 1 Weak to it."
        public override string DescriptionInner()
        {
            return "The leftmost non-Precision attack in your hand gains Precision.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            var leftmostAttack = BattleHelpers.LeftmostCardInHandThat(
                item => item.CardType == CardType.AttackCard 
                && !item.HasDamageModifier<PrecisionDamageModifier>());
            if (leftmostAttack != null)
            {
                leftmostAttack.DamageModifiers.Add(new PrecisionDamageModifier());
            }
        }
    }
}