using System.Collections;
using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;

namespace Assets.CodeAssets.Cards.HammerCards.Rare
{
    public class HairTriggerTemper : AbstractCard
    {
        // Whenever you Taunt an enemy, gain 2 Retaliate.

        public HairTriggerTemper()
        {
            this.SoldierClassCardPools.Add(typeof(HammerSoldierClass));

            SetCommonCardAttributes("Hair Trigger Temper", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1);
            ProtoSprite = ProtoGameSprite.HammerIcon("volcano");

        }

        public override string DescriptionInner()
        {
            return $"Whenever you taunt an enemy, gain 2 retaliate.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {

        }
    }
}