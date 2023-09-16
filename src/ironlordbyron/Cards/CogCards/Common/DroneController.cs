using Assets.CodeAssets.Cards.CogCards.Special;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Common
{
    public class DroneController : AbstractCard
    {
        // add a Shield Drone to your hand.  Discharge: Add another.  Cost 1.
        public DroneController()
        {
            SetCommonCardAttributes("Drone Controller", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);
            ProtoSprite = ProtoGameSprite.CogIcon("delivery-drone");
        }

        public override string DescriptionInner()
        {
            return "Add a Shield Drone to your hand.  Discharge: Add another.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().CreateCardToHand(new ShieldDrone());
            CardAbilityProcs.Discharge(this, () =>
            {
                action().CreateCardToHand(new ShieldDrone());
            });
        }
    }
}