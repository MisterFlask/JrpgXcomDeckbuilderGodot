using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class ForesightStatusEffect : AbstractStatusEffect
    {
        // this character gains 10 block each time 3 cards are played in a turn.

        public ForesightStatusEffect()
        {
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("brass-eye");

            Name = "Foresight";
        }

        public override string Description => "Whenever 3 cards are played, this character gains [stacks] block.";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool cardIsOwnedByMe)
        {
            SecondaryStacks++;
            if (SecondaryStacks >= 3)
            {
                SecondaryStacks -= 3;
                ActionManager.Instance.ApplyDefense(OwnerUnit, null, Stacks);
            }
        }
    }
}