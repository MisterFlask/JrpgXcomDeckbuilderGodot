using Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.ChessCourt
{
    public class ConscriptedPawn : AbstractEnemyUnit
    {
        public ConscriptedPawn()
        {
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Golems Eye Golem");
            this.Description = "???";
            this.EnemyFaction = EnemyFaction.CHESSCOURT;
            CharacterNicknameOrEnemyName = "Pawn";
            this.MaxHp = 21;
            this.StatusEffects = new List<AbstractStatusEffect>()
            {
                new CowardiceStatusEffect()
            };
            //Big attack, but has to charge up first
        }

        public override void AssignStatusEffectsOnCombatStart()
        {
           
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.RandomIntent(
                IntentsFromBaseDamage.AttackRandomPc(this, 4,2),
                IntentsFromBaseDamage.DefendSelf(this, 5));
        }

    }
}