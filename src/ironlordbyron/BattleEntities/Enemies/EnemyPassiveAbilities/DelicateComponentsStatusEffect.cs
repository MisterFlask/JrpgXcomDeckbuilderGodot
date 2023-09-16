using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class DelicateComponentsStatusEffect : AbstractStatusEffect
    {
        public override string Description => "When this unit dies, add a Common Augment into your inventory.  Removed if this enemy takes >[Stacks] damage on a single turn.";

        public DelicateComponentsStatusEffect()
        {
            Name = "Delicate Components";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("pocket-watch");

        }

        public override void OnTurnStart()
        {
            SecondaryStacks = 0;
        }

        public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
        {
            SecondaryStacks += totalDamageTaken;
            if (SecondaryStacks >= Stacks)
            {
                Stacks = 0; // remove Delicate omponents
            }
        }

        public override void OnDeath(AbstractBattleUnit unitThatKilledMe, AbstractCard cardUsedIfAny)
        {
            ActionManager.Instance.AddAugmentToInventory(PerkAndAugmentationRegistrar.GetRandomAugmentation(Rarity.COMMON));
        }
    }
}