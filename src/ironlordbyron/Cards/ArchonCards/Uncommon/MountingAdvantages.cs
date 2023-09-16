using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Common
{
    public class MountingAdvantages : AbstractCard
    {
        public MountingAdvantages()
        {
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            this.SetCommonCardAttributes(
                "Mounting Advantages",
                Rarity.UNCOMMON,
                TargetType.ALLY,
                CardType.PowerCard,
                3,
                protoGameSprite: ProtoGameSprite.ArchonIcon("mountaintop")
                );
            this.PersistentCostModifiers.Add(new PlannedCostModifier());

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