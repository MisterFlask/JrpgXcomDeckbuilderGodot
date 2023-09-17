using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Missions
{
    public class AugmentMissionReward : AbstractMissionReward
    {
        public AugmentMissionReward()
        {
            ProtoSprite = ProtoGameSprite.MissionIcon("relic");
        }

        public override string GenericDescription()
        {
            return "Gain an augment to your inventory";
        }

        public override void OnReward()
        {
            GameState.Instance.AugmentationInventory.Add(GetRandomAugmentationAsReward());
        }

        public AbstractSoldierPerk GetRandomAugmentationAsReward()
        {
            return new AgilePerk(); 
        }
    }
}