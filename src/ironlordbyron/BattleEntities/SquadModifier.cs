using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities
{
    /// <summary>
    /// These are attached to squads in particular acts.  ModifySquad is applied once at beginning of combat.
    /// </summary>
    public interface SquadModifier
    {
        void ModifySquad(List<AbstractBattleUnit> enemyCharacters);
    }
    public class StrongSquadMod : SquadModifier
    {
        int Stacks { get; set; } = 5;

        public void ModifySquad(List<AbstractBattleUnit> enemyCharacters)
        {
            foreach(var character in enemyCharacters)
            {
                character.ApplyStatusEffect<StrengthStatusEffect>(new StrengthStatusEffect(), Stacks );
            }
        }
    }
}
