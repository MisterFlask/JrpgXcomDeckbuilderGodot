using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class AboveItAll : AbstractStatusEffect
    {
        public AboveItAll()
        {
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("royal-love");
            Name = "AboveItAll";
        }
        public override string Description => "This character only takes damage from attacks.";

        public override void ModifyPostBlockDamageTaken(DamageBlob damageBlob)
        {
            if (!damageBlob.IsAttackDamage)
            {
                damageBlob.Damage = 0;
            }
        }
    }
}