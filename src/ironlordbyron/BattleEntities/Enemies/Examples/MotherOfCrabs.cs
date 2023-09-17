using Assets.CodeAssets.BattleEntities.Intents;
using System.Collections;
using System.Collections.Generic;

namespace Assets.CodeAssets.BattleEntities.Enemies
{
    public class MotherOfCrabs : AbstractEnemyUnit
    {

        public MotherOfCrabs()
        {
            this.MaxHp = 100;
            
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return new SummonEnemiesOrElseHealIntent(this, new BrainCrab()).ToSingletonList<AbstractIntent>();
        }
    }
}