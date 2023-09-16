using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.GameLogic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Common
{
    public class BloodSwarm : AbstractCard
    {
        public BloodSwarm()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Bloodswarm", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 1);
            this.CardTags.Add(BattleCardTags.SWARM);
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
                this.CorrespondingPermanentCard().BaseDamage++;
            }
            action().AttackUnitForDamage(target, this.Owner, BaseDamage, this);
        }

        public override void InHandAtEndOfTurnAction()
        {
        }

        public override void OnManualDiscard()
        {
            CardAbilityProcs.ProcNascent(this);
        }

        // Deal 6 damage and gain 2 damage for the rest of the combat.  Bloodprice.  Swarm. If bloodprice is paid, 
    }
}