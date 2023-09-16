using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.Efficiency
{
    public class EfficiencyOrganicAmbassador : AbstractEnemyUnit
    {
        public EfficiencyOrganicAmbassador()
        {
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Boss Flynn The Original Slime");

            this.ProtoSprite = ProtoGameSprite.MachineBattler("Library Book Master");
            this.Description = "???";
            this.CharacterNicknameOrEnemyName = "Organic Ambassador";
            this.EnemyFaction = EnemyFaction.EFFICIENCY;
            this.ApplyStatusEffect(new ToughnessStatusEffect(), stacks: 3);
            UnitSize = UnitSize.MEDIUM;
            this.MaxHp = 44;
        }

        //Increase stress of all characters by 10 => attack for 20%

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromPercentBase.AttackRandomPcWithDebuff(new StressStatusEffect { Stacks = 10}, this, 50, 1),
                IntentsFromPercentBase.StatusEffectToRandomPc(this, new StressStatusEffect(), 30));
        }
    }
}