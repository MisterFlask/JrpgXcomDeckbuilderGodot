using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.CostModifiers;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Uncommon
{
    public class MountingAdvantages : AbstractCard
    {
        public MountingAdvantages()
        {
            SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            SetCommonCardAttributes(
                "Mounting Advantages",
                Rarity.UNCOMMON,
                TargetType.ALLY,
                CardType.PowerCard,
                3,
                protoGameSprite: ProtoGameSprite.ArchonIcon("mountaintop")
                );
            PersistentCostModifiers.Add(new PlannedCostModifier());

        }


        public override string DescriptionInner()
        {
            return $"OTHER allies gain +1 strength per turn.  Planned.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new AdvancedStatusEffect(), 1);
        }
    }
}