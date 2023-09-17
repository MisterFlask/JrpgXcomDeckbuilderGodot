using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Efficiency
{
    public class EfficiencyOrganicAmbassador : AbstractEnemyUnit
    {
        public EfficiencyOrganicAmbassador()
        {
            ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Boss Flynn The Original Slime");

            ProtoSprite = ProtoGameSprite.MachineBattler("Library Book Master");
            Description = "???";
            CharacterNicknameOrEnemyName = "Organic Ambassador";
            EnemyFaction = EnemyFaction.EFFICIENCY;
            ApplyStatusEffect(new ToughnessStatusEffect(), stacks: 3);
            UnitSize = UnitSize.MEDIUM;
            MaxHp = 44;
        }

        //Increase stress of all characters by 10 => attack for 20%

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromPercentBase.AttackRandomPcWithDebuff(new StressStatusEffect { Stacks = 10 }, this, 50, 1),
                IntentsFromPercentBase.StatusEffectToRandomPc(this, new StressStatusEffect(), 30));
        }
    }
}