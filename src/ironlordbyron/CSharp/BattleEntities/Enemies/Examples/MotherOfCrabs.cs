using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Examples
{
    public class MotherOfCrabs : AbstractEnemyUnit
    {

        public MotherOfCrabs()
        {
            MaxHp = 100;

        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return new SummonEnemiesOrElseHealIntent(this, new BrainCrab()).ToSingletonList<AbstractIntent>();
        }
    }
}