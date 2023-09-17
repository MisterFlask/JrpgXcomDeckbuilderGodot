using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Summer
{
    public class ParanoidSycophant : AbstractBattleUnit
    {
        public ParanoidSycophant()
        {
            CharacterNicknameOrEnemyName = "Paranoid Sycophant";
            EnemyFaction = EnemyFaction.SUMMER;
            SquadRole = EnemySquadRole.SMALL;
            ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Demon Critter Squirrel");

            MaxHp = 30;

        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromBaseDamage.AttackRandomPc(this, 7, 2),
                IntentsFromBaseDamage.DefendSelf(this, 10));
        }

        public override void AssignStatusEffectsOnCombatStart()
        {
            StatusEffects.Add(new AppalledStatusEffect
            {
                Stacks = 1
            });
        }
    }
}