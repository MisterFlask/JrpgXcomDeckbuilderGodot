using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.Efficiency
{
    public class EfficiencyMobileRestockUnit : AbstractEnemyUnit
    {
        public EfficiencyMobileRestockUnit()
        {
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Boss Eldritch Slime Overmind");
            this.Description = "???";
            this.CharacterNicknameOrEnemyName = "Mobile Restock Unit";
            this.EnemyFaction = EnemyFaction.EFFICIENCY;
            this.MaxHp = 55;
            this.ApplyStatusEffect(new ArmoredStatusEffect(), stacks: 1);
            UnitSize = UnitSize.SMALL;
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromPercentBase.AttackRandomPc(
                    this,
                    50,
                    1),
                IntentsFromPercentBase.BuffOther(this, new StrengthStatusEffect(), stacks:2));
        }
    }
}