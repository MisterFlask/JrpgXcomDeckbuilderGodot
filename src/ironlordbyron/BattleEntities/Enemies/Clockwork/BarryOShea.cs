using Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities;
using System.Collections;
using System.Collections.Generic;

namespace Assets.CodeAssets.BattleEntities.Enemies.Summer
{
    public class BarryOShea : AbstractBattleUnit
    {
        public BarryOShea()
        {
            CharacterNicknameOrEnemyName = "Barry O'Shea";
            // You might need to set EnemyFaction and SquadRole based on your game's requirements.
            // I'm using placeholders here for illustration.
            EnemyFaction = EnemyFaction.SUMMER;
            SquadRole = EnemySquadRole.LARGE;

            // Placeholder for sprite. You might want to provide a proper path to Barry O'Shea's sprite.
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites\\Enemies\\v2\\Boss Khronos"); //path: "Sprites/Enemies/PathToBarrysSprite"

            MaxHp = 300;

            // Adding the status effects/buffs for Barry O'Shea.
            this.StatusEffects.Add(new NoRush { Stacks = 2 });
            this.StatusEffects.Add(new Growing { Stacks = 2});
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromBaseDamage.AttackRandomPc(this, 15, 3),
                IntentsFromBaseDamage.AttackAllPcs(this, 10, 1),
                IntentsFromBaseDamage.DefendSelf(this, 15)
            );
        }
    }
}