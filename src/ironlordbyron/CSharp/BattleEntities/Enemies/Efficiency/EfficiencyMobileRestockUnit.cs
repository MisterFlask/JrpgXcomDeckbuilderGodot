using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Efficiency
{
    public class EfficiencyMobileRestockUnit : AbstractEnemyUnit
    {
        public EfficiencyMobileRestockUnit()
        {
            ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Boss Eldritch Slime Overmind");
            Description = "???";
            CharacterNicknameOrEnemyName = "Mobile Restock Unit";
            EnemyFaction = EnemyFaction.EFFICIENCY;
            MaxHp = 55;
            ApplyStatusEffect(new ArmoredStatusEffect(), stacks: 1);
            UnitSize = UnitSize.SMALL;
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromPercentBase.AttackRandomPc(
                    this,
                    50,
                    1),
                IntentsFromPercentBase.BuffOther(this, new StrengthStatusEffect(), stacks: 2));
        }
    }
}