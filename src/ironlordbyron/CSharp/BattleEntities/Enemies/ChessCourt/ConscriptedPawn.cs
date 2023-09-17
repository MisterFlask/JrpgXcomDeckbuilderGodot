using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities;
using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.ChessCourt
{
    public class ConscriptedPawn : AbstractEnemyUnit
    {
        public ConscriptedPawn()
        {
            ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Golems Eye Golem");
            Description = "???";
            EnemyFaction = EnemyFaction.CHESSCOURT;
            CharacterNicknameOrEnemyName = "Pawn";
            MaxHp = 21;
            StatusEffects = new List<AbstractStatusEffect>()
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
                IntentsFromBaseDamage.AttackRandomPc(this, 4, 2),
                IntentsFromBaseDamage.DefendSelf(this, 5));
        }

    }
}