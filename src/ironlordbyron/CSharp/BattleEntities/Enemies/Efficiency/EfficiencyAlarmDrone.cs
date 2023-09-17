using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Efficiency
{

    // defends twice, then flees.  This increases the Doom Counter by 2.
    public class EfficiencyAlarmDrone : AbstractEnemyUnit
    {
        public EfficiencyAlarmDrone()
        {
            ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Slime RPG Basic");
            Description = "???";
            CharacterNicknameOrEnemyName = "Alarm Drone";
            MaxHp = 22;
            EnemyFaction = EnemyFaction.EFFICIENCY;
            UnitSize = UnitSize.SMALL;
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromPercentBase.AttackRandomPc(
                    this,
                    50,
                    1),
                IntentsFromPercentBase.AttackRandomPc(
                    this,
                    50,
                    1),
                IntentsFromPercentBase.DefendSelf(
                    this,
                    50),
                IntentsFromPercentBase.DoMagic(this, () =>
                {
                    ActionManager.Instance.IncrementDoomCounter(2);
                    ActionManager.Instance.KillUnit(this);
                })
            );
        }
    }
}