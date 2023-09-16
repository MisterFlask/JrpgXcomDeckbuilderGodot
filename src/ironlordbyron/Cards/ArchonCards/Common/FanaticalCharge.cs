using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Common
{
    public class FanaticalCharge : AbstractCard
    {
        public FanaticalCharge()
        {
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            this.SetCommonCardAttributes(
                "Fanatical Charge",
                Rarity.COMMON,
                TargetType.NO_TARGET_OR_SELF,
                CardType.AttackCard,
                1,
                protoGameSprite: ProtoGameSprite.ArchonIcon("mounted-knight")
                );
            this.BaseDamage = 5;
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage to a random enemy.  Exert. Add a Manuever to your hand.";
        }

        public override void OnPlay(AbstractBattleUnit _t, EnergyPaidInformation energyPaid)
        {
            var target = CardTargeting.RandomTargetableEnemy();
            action().AttackUnitForDamage(target, Owner, BaseDamage, this);
            CardAbilityProcs.ProcExert(this);
            action().ApplyStatusEffect(target, new AdvancedStatusEffect(), 1);
        }
    }
}