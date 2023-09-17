using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.CodeAssets.BattleEntities.Enemies
{

    public class SensorDrone : AbstractEnemyUnit
    {
        public SensorDrone()
        {
            this.CharacterFullName = "Sensor Drone";
            this.MaxHp = 30;
        }

        public override List<AbstractIntent> GetNextIntents()
        {

            return new List<AbstractIntent>
            {
                SingleUnitAttackIntent.AttackRandomPc(this, 5, 1)
            };
        }

        public override void OnCombatStart()
        {
            action().ApplyStatusEffect(this, new MonitoringStatusEffect(), 1);
            action().ApplyStatusEffect(this, new ResonanceSensitivityStatusEffect(), 15);
        }
    }

    public class ResonanceSensitivityStatusEffect : AbstractStatusEffect
    {
        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit target, bool ownedByMe)
        {
            if (cardPlayed.BaseEnergyCost() == 3)
            {
                action().DamageUnitNonAttack(this.OwnerUnit, null, Stacks);
            }
        }

        public override string Description => $"Whenever a 3 base-energy-cost card is played, this takes {Stacks} damage.";
    }

    public class MonitoringStatusEffect : AbstractStatusEffect
    {
        public MonitoringStatusEffect()
        {
            Name = "Monitoring";
        }

        public override string Description => "Whenever a cost 1 card is played, grants +1 strength to a random other unit in party";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool ownedByMe)
        {
            if (cardPlayed.BaseEnergyCost() == 1)
            {
                var anotherUnitInEnemyParty = GameState.Instance
                    .EnemyUnitsInBattle
                    .Shuffle()
                    .First(item => !item.IsDead);

                ActionManager.Instance.ApplyStatusEffect(anotherUnitInEnemyParty, new StrengthStatusEffect(), Stacks);

            }
        }
    }
}