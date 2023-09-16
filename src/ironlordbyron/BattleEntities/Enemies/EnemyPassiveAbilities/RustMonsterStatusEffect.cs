using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class RustMonsterStatusEffect : AbstractStatusEffect
    {
        public RustMonsterStatusEffect()
        {
            Name = "Corrosive";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("rusty-sword");
        }
        public override string Description => "Whenever this character is attacked, the card attacking it gets -[stacks] damage for the rest of combat.";

        public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
        {
            if (cardUsedIfAny != null)
            {
                cardUsedIfAny.BaseDamage -= Stacks;
                if (cardUsedIfAny.BaseDamage < 0)
                {
                    cardUsedIfAny.BaseDamage = 0;
                }
            }
        }
    }
}