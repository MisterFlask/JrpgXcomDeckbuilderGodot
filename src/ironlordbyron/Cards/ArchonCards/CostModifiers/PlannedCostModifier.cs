using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Effects
{
    public class PlannedCostModifier : AbstractCostModifier
    {

        public override int GetCostModifier()
        {
            if (BattleHelpers.DoesAnyEnemyHave<MarkedStatusEffect>())
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}