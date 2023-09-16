using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Powers
{
    public class ChaosEngineer : AbstractCard
    {
        // Whenever you play a card that targets an enemy, apply 2 Burning to that enemy.
        public ChaosEngineer()
        {
            SetCommonCardAttributes("Chaos Engineer", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);
            ProtoSprite = ProtoGameSprite.BlackhandIcon("radial-balance");
        }

        public override string DescriptionInner()
        {
            return "Whenever you play a card that targets an enemy, apply 2 Burning to that enemy.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new ChaosEngineerStatusEffect(), 2);
            this.Action_Exhaust();
        }
    }

    public class ChaosEngineerStatusEffect : AbstractStatusEffect
    {
        public ChaosEngineerStatusEffect()
        {
            this.Name = "Chaos Engineer";
        }

        public override string Description => $"Whenever a card is played targeting an enemy, apply {DisplayedStacks()} Burning to it.";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool isMine)
        {
            if (targetOfCard != null && targetOfCard.IsEnemy)
            {
                action().ApplyStatusEffect(targetOfCard, new BurningStatusEffect(), Stacks);
            }
        }
    }
}