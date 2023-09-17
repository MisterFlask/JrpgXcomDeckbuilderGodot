using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Common
{
    public class FanaticalCharge : AbstractCard
    {
        public FanaticalCharge()
        {
            SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            SetCommonCardAttributes(
                "Fanatical Charge",
                Rarity.COMMON,
                TargetType.NO_TARGET_OR_SELF,
                CardType.AttackCard,
                1,
                protoGameSprite: ProtoGameSprite.ArchonIcon("mounted-knight")
                );
            BaseDamage = 5;
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage to a random enemy.  Exert. Add a Manuever to your hand.";
        }

        public override void OnPlay(AbstractBattleUnit _t, EnergyPaidInformation energyPaid)
        {
            var target = CardTargeting.RandomTargetableEnemy();
            action().AttackUnitForDamage(target, Owner, BaseDamage, this);
            this.ProcExert();
            action().ApplyStatusEffect(target, new AdvancedStatusEffect(), 1);
        }
    }
}