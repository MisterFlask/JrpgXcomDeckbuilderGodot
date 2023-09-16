using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class FlyingStatusEffect : AbstractStatusEffect
    {
        public FlyingStatusEffect()
        {
            Name = "Flying";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("liberty-wing");

        }

        public override string Description => "Deals [stacks] extra damage.  Remove Flight after being hit three times in a turn.";

        public override void Init()
        {
            SecondaryStacks = Stacks;
        }

        public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
        {
            SecondaryStacks--;
            if (SecondaryStacks == 0)
            {
                Stacks = 0;
            }
        }
        public override int DamageDealtAddition()
        {
            return Stacks;
        }

        public override void OnTurnStart()
        {
            SecondaryStacks = Stacks;
            OwnerUnit.RemoveStatusEffect<FlightEffectOnFirstHitTakenThisTurn>();
            OwnerUnit.ApplyStatusEffect(new FlightEffectOnFirstHitTakenThisTurn(), 1);
        }
    }

    public class FlightEffectOnFirstHitTakenThisTurn : AbstractStatusEffect
    {
        public FlightEffectOnFirstHitTakenThisTurn()
        {
            Name = "Evasive (Flying)";
        }

        public override string Description => "The next attack this turn deals 1 damage.";

        public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
        {
            Stacks--;
        }
    }
}