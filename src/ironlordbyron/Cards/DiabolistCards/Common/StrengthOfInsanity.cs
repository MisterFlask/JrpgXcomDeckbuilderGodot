using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.Stickers;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Common
{
    public class StrengthOfInsanity : AbstractCard
    {
        // Erosion card.  Unplayable.  Startup: Gain 1 strength

        public StrengthOfInsanity()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Strength of Insanity", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.ErosionCard, 0);
            this.Unplayable = true;
            this.Stickers.Add(new HazardousCardSticker());
            ProtoSprite = ProtoGameSprite.DiabolistIcon("horned-skull");
        }

        public override string DescriptionInner()
        {
            return $"Unplayable.  Startup: {ownerDisplayString()} gain 1 strength.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
        }

        public override void OnStartup()
        {
            action().ApplyStatusEffect(Owner, new StrengthStatusEffect(), 1);
        }

    }
}