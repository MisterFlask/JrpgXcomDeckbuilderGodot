using System.Collections;
using System;

public static class CardActions
{
    public static void ActOnAllEnemies(Action<AbstractBattleUnit> act )
    {
        foreach(var item in GameState.Instance.EnemyUnitsInBattle)
        {
            act(item);
        }
    }
}
