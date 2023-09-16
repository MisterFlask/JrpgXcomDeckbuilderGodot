using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Rare
{
    public class Gluttony : AbstractCard
    {
        // Deal 12 damage.  Gain Vampiric 3.  Exhaust.
        //  [whenever you play an attack, gain Stacks hp.  Ticks down each turn]

        public Gluttony()
        {
            this.SetCommonCardAttributes("Gluttony", Rarity.RARE, TargetType.ENEMY, CardType.AttackCard, 1, typeof(DiabolistSoldierClass));
            this.BaseDamage = 12;
        }

        public override string DescriptionInner()
        {
            return $"Deal {BaseDamage} damage.  Gain Vampiric 3.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().AttackUnitForDamage(target, this.Owner, BaseDamage, this);
            action().ApplyStatusEffect(Owner, new VampiricStatusEffect(), 3);
            this.Action_Exhaust();
        }
    }


    public class VampiricStatusEffect : AbstractStatusEffect
    {
        public override string Description => $"Ticks down at the start of each turn.  " +
            $"When this character strikes an enemy, it gains {DisplayedStacks()} HP.";

        public override void OnStriking(AbstractBattleUnit unitStruck, AbstractCard cardPlayedIfAny, int totalDamageDealt)
        {
            action().ApplyStatusEffect(unitStruck, new VampiricStatusEffect(), 3);
        }

        public override void OnTurnEnd()
        {
            action().TickDownStatusEffect<VampiricStatusEffect>(this.OwnerUnit);
        }
    }
}