using System;
using System.Collections;
using System.Collections.Generic;
using Godot;
namespace Assets.CodeAssets.BattleEntities.Intents
{
    public class SummonEnemiesOrElseHealIntent : AbstractIntent
    {

        public AbstractBattleUnit MinionToCreate;
        public SummonEnemiesOrElseHealIntent(AbstractBattleUnit source, 
            AbstractBattleUnit minionToCreate) : base(source, null, IntentIcons.MagicIntent)
        {
            this.MinionToCreate = minionToCreate;
        }

        public override string GetGenericDescription()
        {
            return "This enemy intends to summon allies next round.  If there aren't spots available, will heal 1/3 of its HP.  Current status: " + GetCurrentStatusDescription();
        }

        private string GetCurrentStatusDescription()
        {
            if (CanSummon())
            {
                return "Will summon a unit.";
            }
            else
            {
                return "Will heal itself.";
            }
        }

        public override string GetOverlayText()
        {
            return "";
        }

        protected override void Execute()
        {
            if (CanSummon())
            {
                ActionManager.Instance.CreateEnemyMinionInBattle(MinionToCreate);
            }
            else 
            {
                ActionManager.Instance.HealUnit(Source, Source.MaxHp / 3);
            }
        }

        private bool CanSummon()
        {
            return BattleScreenPrefab.INSTANCE.IsRoomForAnotherEnemy();
        }

        protected override bool CurrentlyAvailableForUsage()
        {
            return true;
        }

        protected override IntentPrefab GeneratePrefab(Node2D parent)
        {
            throw new System.NotImplementedException();
        }
    }
}