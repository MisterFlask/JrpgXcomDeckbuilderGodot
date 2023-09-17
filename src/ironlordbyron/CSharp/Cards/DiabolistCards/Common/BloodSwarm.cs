using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic.BattleRules;
using System.Linq;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Common
{
    public class BloodSwarm : AbstractCard
    {
        public BloodSwarm()
        {
            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            SetCommonCardAttributes("Bloodswarm", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 1);
            CardTags.Add(BattleCardTags.SWARM);
            BaseDamage = 6;
            ProtoSprite = ProtoGameSprite.DiabolistIcon("wasp-sting");
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage.  " +
                "Bloodprice.  " +
                "Swarm. " +
                "If bloodprice is paid, gains 1 damage PERMANENTLY.  Nascent.";
        }

        public override EnergyPaidInformation GetNetEnergyCost()
        {
            return BloodpriceBattleRules.GetNetEnergyCostWithBloodprice(this);
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            if (energyPaid.ActionsToTake.Any(item => item is BloodpricePaidAction))
            {
                CorrespondingPermanentCard().BaseDamage++;
            }
            action().AttackUnitForDamage(target, Owner, BaseDamage, this);
        }

        public override void InHandAtEndOfTurnAction()
        {
        }

        public override void OnManualDiscard()
        {
            this.ProcNascent();
        }

        // Deal 6 damage and gain 2 damage for the rest of the combat.  Bloodprice.  Swarm. If bloodprice is paid, 
    }
}