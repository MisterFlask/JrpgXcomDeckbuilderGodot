using Assets.CodeAssets.BattleEntities.Intents;
using System.Collections;
using System.Collections.Generic;

namespace Assets.CodeAssets.BattleEntities.Enemies.ChessCourt
{
    /// <summary>
    /// Summons a Conscripted Pawn every turn.  
    /// If there's at least 3 Conscripted Pawns out, she buffs ALL of them for 4.
    /// </summary>
    public class Subqueen : AbstractEnemyUnit
    {

        public Subqueen()
        {
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites\\Enemies\\v2\\Hero Magic Knightess");
            MaxHp = 100;
            CharacterNicknameOrEnemyName = "Subqueen";
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            if (GameState.Instance.EnemyUnitsInBattle.Count < 3)
            {
                return new MagicIntent(this, ()=>
                {
                    ActionManager.Instance.CreateEnemyMinionInBattle(new ConscriptedPawn());
                }).ToSingletonList<AbstractIntent>();
            }
            else
            {
                return new MagicIntent(this, () =>
                {
                    foreach(var enemy in GameState.Instance.EnemyUnitsInBattle)
                    {
                        ActionManager.Instance.ApplyStatusEffect(enemy, new StrengthStatusEffect { Stacks = 3 });
                    }
                }).ToSingletonList<AbstractIntent>();
            }
        }
    }
}