using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class AttackMultipleIntent
{
    public static List<AbstractIntent> AttackingAllAllies(AbstractBattleUnit source, int damage, int timesStruck)
    {
        return GameState.Instance.AllyUnitsInBattle.Select(target => new SingleUnitAttackIntent(source, target, damage, timesStruck) as AbstractIntent).ToList();
    }


}
