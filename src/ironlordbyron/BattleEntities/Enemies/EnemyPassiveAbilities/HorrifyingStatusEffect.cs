using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class HorrifyingStatusEffect : AbstractStatusEffect
    {
        public HorrifyingStatusEffect()
        {
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("eyeball");

            Name = "Horrifying";
        }

        public override string Description => "Whenever this character is the target of a card, the targeting character gains [stacks] stress.";

        public override void OnTargetedByCard(AbstractCard sourceCard)
        {
            ActionManager.Instance.ApplyStress(OwnerUnit, stressApplied: Stacks);
        }
    }
}