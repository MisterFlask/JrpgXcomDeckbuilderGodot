using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects;
using GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic.BattleRules;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.CostModifiers
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