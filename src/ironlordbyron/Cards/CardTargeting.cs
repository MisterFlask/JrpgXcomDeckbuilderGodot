using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards
{
    public static class CardTargeting
    {
        public static IEnumerable<AbstractBattleUnit> TargetableEnemies()
        {
            return GameState.Instance.EnemyUnitsInBattle
                .Where(item => !item.IsDead);
        }

        public static AbstractBattleUnit RandomTargetableEnemy()
        {
            return TargetableEnemies().PickRandom();
        }

    }
}