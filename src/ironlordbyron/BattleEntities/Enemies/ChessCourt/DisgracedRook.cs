using Assets.CodeAssets.BattleEntities.Enemies.Efficiency;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.ChessCourt
{
    public class DisgracedRook : AbstractEnemyUnit
    {
        public DisgracedRook()
        {
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Boss RADIANCE Eldritch Knight");
            this.Description = "???";
            this.EnemyFaction = EnemyFaction.CHESSCOURT;
            CharacterNicknameOrEnemyName = "Disgraced Rook";
            this.MaxHp = 65;
            //Big attack, but has to charge up first
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.RandomIntent(
                IntentsFromBaseDamage.AttackRandomPc(this, 12, 1),
                IntentsFromBaseDamage.DefendSelf(this, 5),
                IntentsFromBaseDamage.DebuffRandomOther(this, new WeakenedStatusEffect()));
        }

        public override void AssignStatusEffectsOnCombatStart()
        {
            StatusEffects.Add(new DisgracedRookStatusEffect());
        }
    }

    public class DisgracedRookStatusEffect: AbstractStatusEffect
    {
        public DisgracedRookStatusEffect()
        {
            Name = "Fallen from Glory";
        }

        public override string Description => @"Every turn this character takes less than [stacks] damage, summon a Conscripted Pawn.
  Otherwise, grant 2 strength to all enemies.";

        public override void OnTurnStart()
        {
            if (SecondaryStacks < Stacks)
            {
                ActionManager.Instance.CreateEnemyMinionInBattle(new ConscriptedPawn());
            }
            else
            {
                foreach(var character in GameState.Instance.EnemyUnitsInBattle)
                {
                    ActionManager.Instance.ApplyStatusEffect(character, new StrengthStatusEffect(), 2);
                }
            }
            SecondaryStacks = 0;
        }
    }
}