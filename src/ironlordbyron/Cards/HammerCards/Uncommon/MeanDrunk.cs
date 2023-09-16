using Assets.CodeAssets.BattleEntities.StatusEffects;
using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.HammerCards.Common;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards.Uncommon
{
    public class MeanDrunk : AbstractCard
    {
        // Gain 5 Rage, plus another 1 per Barricade you have. Draw a card.  Cost 1.  
        // Add a Transient Hurtful Words and Gestures to your hand.

        public MeanDrunk()
        {
            SetCommonCardAttributes("Mean Drunk", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1, typeof(HammerSoldierClass));
            ProtoSprite = ProtoGameSprite.HammerIcon("angry-eyes");

        }

        public override string DescriptionInner()
        {
            return $"Gain 5 Rage, plus another 1 per Barricade you have. Add a Hurtful Words and Gestures to your hand.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToOwner(new RageStatusEffect(), 5 + GetStacksOf<BarricadeStatusEffect>());
            action().CreateCardToHand(new HurtfulWordsAndGestures());
        }
    }
}