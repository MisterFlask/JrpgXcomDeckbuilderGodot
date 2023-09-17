using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.BadCards
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