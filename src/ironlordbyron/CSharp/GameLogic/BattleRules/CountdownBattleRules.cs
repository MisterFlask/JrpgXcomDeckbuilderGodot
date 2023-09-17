using System;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic.BattleRules
{
    public class CountdownBattleRules
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