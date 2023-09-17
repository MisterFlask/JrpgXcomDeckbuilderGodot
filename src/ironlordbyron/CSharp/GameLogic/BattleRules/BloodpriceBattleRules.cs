using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards;
using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic.BattleRules
{
    public class BloodpriceBattleRules
    {
        public static EnergyPaidInformation GetNetEnergyCostWithBloodprice(AbstractCard card)
        {
            if (GameState.Instance.energy < card.GetDisplayedEnergyCost())
            {
                var missingEnergy = card.GetDisplayedEnergyCost() - GameState.Instance.energy;

                return new EnergyPaidInformation()
                {
                    ActionsToTake = new List<EnergyPaidAction>
                    {
                        new BloodpricePaidAction
                        {
                            LifePaid = 5 * missingEnergy
                        }
                    },
                    EnergyCost = GameState.Instance.energy
                };
            }

            return new EnergyPaidInformation()
            {
                EnergyCost = card.GetDisplayedEnergyCost()
            };
        }
    }

    public class BloodpricePaidAction : EnergyPaidAction
    {
        public int LifePaid { get; set; } = 0;
        public override void OnCardPlayed(AbstractCard card)
        {
            ActionManager.Instance.DamageUnitNonAttack(card.Owner, null, LifePaid);
        }
    }
}