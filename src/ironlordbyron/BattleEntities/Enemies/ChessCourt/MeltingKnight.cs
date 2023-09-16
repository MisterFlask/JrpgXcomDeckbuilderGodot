using Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.ChessCourt
{
    /// <summary>
    /// Difficulty 3
    /// </summary>
    public class MeltingKnight : AbstractEnemyUnit
    {
        public MeltingKnight()
        {
            // difficulty 2
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Boss Shield Knightess");
            this.Description = "???";
            this.EnemyFaction = EnemyFaction.CHESSCOURT;
            this.UnitSize = UnitSize.MEDIUM;
            CharacterNicknameOrEnemyName = "Red Knight";
            this.MaxHp = 55;
        }

        public override void AssignStatusEffectsOnCombatStart()
        {
            // todo
            StatusEffects.Add(new ArmoredStatusEffect()
            {
                Stacks = 2
            });

            StatusEffects.Add(new Fearsome()
            {
                Stacks = 2
            });
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.RandomIntent(
                IntentsFromBaseDamage.AttackRandomPc(this, 7, 2),
                IntentsFromBaseDamage.AttackRandomPc(this, 15, 1),
                IntentsFromBaseDamage.DefendSelf(this, 15),
                IntentsFromBaseDamage.BuffSelf(this, new StrengthStatusEffect(), 3));
        }

    }
}