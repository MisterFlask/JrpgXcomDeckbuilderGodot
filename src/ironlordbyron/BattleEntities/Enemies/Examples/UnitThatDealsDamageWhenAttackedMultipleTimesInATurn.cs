using System.Collections;
using System.Collections.Generic;

public class UnitThatDealsDamageWhenAttackedMultipleTimesInATurn : AbstractEnemyUnit
{

    public UnitThatDealsDamageWhenAttackedMultipleTimesInATurn()
    {
        this.CharacterFullName = "UnitThatDealsDamageWhenAttackedMultipleTimesInATurn";
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/RoboVAK", color: Color.white);
        this.MaxHp = 44;
        this.ApplyStatusEffect(new DealsDamageOnAttackMultipleTimesInATurn(), stacks: 4);
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        return new List<List<AbstractIntent>>
        {
            AttackMultipleIntent.AttackingAllAllies(this, 1, 1),
            new BuffSelfIntent(this, new StrengthStatusEffect(), 5).ToSingletonList<AbstractIntent>()
        }
        .PickRandom();
    }

}

public class DealsDamageOnAttackMultipleTimesInATurn : AbstractStatusEffect
{
    public DealsDamageOnAttackMultipleTimesInATurn()
    {
        Name = "Resentment";
        ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/falling-bang", Color.yellow);
    }
    public override string Description => "When attacked for the third time in a turn, deals [stacks] damage to ALL characters.";

    public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
    {
        this.OwnerUnit.ApplyStatusEffect<DealtDamageThisTurnMarker>(new DealtDamageThisTurnMarker(), 1);

        if (OwnerUnit.GetStatusEffect<DealtDamageThisTurnMarker>().Stacks == 3)
        {
            foreach(var combatant in state().AllyUnitsInBattle)
            {
                action().DamageUnitNonAttack(combatant, null, Stacks);
            }
            foreach (var combatant in state().EnemyUnitsInBattle)
            {
                action().DamageUnitNonAttack(combatant, null, Stacks);
            }
        }
    }
}

public class DealtDamageThisTurnMarker : AbstractStatusEffect
{
    //todo
    public DealtDamageThisTurnMarker()
    {
        Name = "Number of times attacked this turn.";
        ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/falling-bang", Color.yellow);
    }

    public override string Description => "Increases when this unit is attacked.";

    public override void OnTurnEnd()
    {
        this.Stacks = 0;
    }

}
