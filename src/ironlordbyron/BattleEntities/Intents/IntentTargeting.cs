﻿using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public static class IntentTargeting
{
    public static AbstractBattleUnit GetRandomLivingPlayerUnit()
    {
        return ServiceLocator.GameState().AllyUnitsInBattle.PickRandomWhere(item => !item.IsDead);
    }

    public static List<AbstractBattleUnit> GetEnemyUnitsOfType<T>() where T : AbstractBattleUnit
    {
        return ServiceLocator.GameState().EnemyUnitsInBattle.Where(item => item.GetType() == typeof(T)).ToList();
    }

    public static AbstractBattleUnit GetOneEnemyUnitOfType<T>() where T : AbstractBattleUnit
    {
        return ServiceLocator.GameState()
            .EnemyUnitsInBattle
            .Where(item => item.GetType() == typeof(T) && !item.IsDead).ToList()
            .PickRandom();
    }
}
