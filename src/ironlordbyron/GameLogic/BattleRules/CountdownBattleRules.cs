using System;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.GameLogic.BattleRules
{
    public class CountdownBattleRules : MonoBehaviour
    {
        public static void RunCountdown(AbstractCard card, Action whenAtZero)
        {
            ActionManager.Instance.PushActionToBack("RunCountdown", () =>
            {
                card.Countdown--;
                if (card.Countdown == 0)
                {
                    whenAtZero();
                }
            });
        }
    }
}