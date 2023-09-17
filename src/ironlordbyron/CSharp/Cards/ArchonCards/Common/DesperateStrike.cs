using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Common
{
    public class DesperateStrike : AbstractCard
    {
        public DesperateStrike()
        {
            SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            SetCommonCardAttributes(
                "Fanatical Charge",
                Rarity.COMMON,
                TargetType.ENEMY,
                CardType.AttackCard,
                1,
                protoGameSprite: ProtoGameSprite.ArchonIcon("bowling-propulsion")
                );
            BaseDamage = 5;
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage to a random enemy.  If I have >40 stress, do it again.  If I have >70 stress, do it again.  Exert.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().AttackUnitForDamage(target, Owner, BaseDamage, this);
            if (Owner.CurrentStress > 40)
            {
                action().AttackUnitForDamage(target, Owner, BaseDamage, this);
            }
            if (Owner.CurrentStress > 70)
            {
                action().AttackUnitForDamage(target, Owner, BaseDamage, this);
            }
            this.ProcExert();
        }
    }
}