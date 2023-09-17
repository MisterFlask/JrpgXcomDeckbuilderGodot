using Assets.CodeAssets.Cards;
using System.Collections;

namespace Assets.CodeAssets.BattleEntities.Enemies.BadCards
{
    public class Winded : AbstractCard
    {
        public Winded()
        {
            /// bog-standard unplayable
            StaticBaseEnergyCost = 0;
        }

        public override string DescriptionInner()
        {
            return "Does nothing.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
        }
    }
}