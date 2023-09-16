using System;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Effects
{
    public static class ArchonBattleRules
    {

        public static void Manuever(AbstractBattleUnit target)
        {
            if (target.HasStatusEffect<AdvancedStatusEffect>())
            {
                ActionManager.Instance.RemoveStatusEffect<AdvancedStatusEffect>(target);
            }
            else
            {
                ActionManager.Instance.ApplyStatusEffect(target, new AdvancedStatusEffect(), 1);
            }
        }
    }
}