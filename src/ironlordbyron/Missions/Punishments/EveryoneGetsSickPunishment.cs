using UnityEngine;
using System.Collections;

public class EveryoneGetsSickPunishment : MissionFailurePunishment
{
    public int Stacks { get; set; }

    public override string Description()
    {
        return "All characters get 4 stacks of Sickness";
    }

    public override void OnFailure()
    {
        GameState.Instance.PersistentCharacterRoster.ForEach(character =>
        {
            character.ApplySoldierPerk(new SickenedSoldierPerk(), 4);
        });
    }

}

public class SickenedSoldierPerk: AbstractSoldierPerk
{
    public override void PerformAtBeginningOfNewDay(AbstractBattleUnit soldierAffected)
    {
        DecrementStacks(soldierAffected);
    }

    public override void PerformAtBeginningOfCombat(AbstractBattleUnit soldierAffected)
    {
        soldierAffected.ApplyStatusEffect(new StrengthStatusEffect(), -1 * Stacks);
    }

    public override string Name()
    {
        return "Sickened";

    }

    public override string Description()
    {
        return "At the beginning of combat, decreases the soldier's strength by the number of stacks";
    }
}


