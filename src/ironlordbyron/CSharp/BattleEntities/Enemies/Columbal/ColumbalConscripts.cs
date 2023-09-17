using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Columbal
{
    public class ColumbalConscripts : AbstractEnemyUnit
    {
        public ColumbalConscripts()
        {
            CharacterNicknameOrEnemyName = "Conscripts";
            EnemyFaction = EnemyFaction.COLUMBAL;
            SquadRole = EnemySquadRole.SMALL;
            ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Lawyers/Conscript");
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromPercentBase.AttackRandomPc(this, 50, 1),
                IntentsFromPercentBase.DefendSelf(this, 50));
        }

        public override void AssignStatusEffectsOnCombatStart()
        {
            StatusEffects.Add(new CowardiceStatusEffect
            {
                Stacks = 4
            });
            StatusEffects.Add(new FlyingStatusEffect
            {
                Stacks = 3
            });
        }

    }
}