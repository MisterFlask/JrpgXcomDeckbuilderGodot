using Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities;
using System.Collections;
using System.Collections.Generic;

namespace Assets.CodeAssets.BattleEntities.Enemies.Summer
{
    public class Naturalist : AbstractBattleUnit
    {
        public Naturalist()
        {
            CharacterNicknameOrEnemyName = "Naturalist";
            EnemyFaction = EnemyFaction.SUMMER;
            SquadRole = EnemySquadRole.REGULAR;
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Dryads Mage");
            MaxHp = 70;
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromBaseDamage.AttackRandomPc(this, 10, 1),
                IntentsFromBaseDamage.DefendSelf(this, 10),
                IntentsFromPercentBase.AttackRandomPcWithDebuff(new WeakenedStatusEffect { Stacks = 3 }, this, 5, 1));
        }

        public override void AssignStatusEffectsOnCombatStart()
        {
            StatusEffects.Add(new FairyGoldStatusEffect
            {
                Stacks = 10
            });
            StatusEffects.Add(new RustMonsterStatusEffect
            {
                Stacks = 3
            });
            StatusEffects.Add(new SoulSuckerStatusEffect
            {
                Stacks = 3
            });
        }
    }
}