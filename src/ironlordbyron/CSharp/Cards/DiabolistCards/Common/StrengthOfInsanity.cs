using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.Stickers;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Common
{
    public class StrengthOfInsanity : AbstractCard
    {
        // Erosion card.  Unplayable.  Startup: Gain 1 strength

        public StrengthOfInsanity()
        {
            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            SetCommonCardAttributes("Strength of Insanity", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.ErosionCard, 0);
            Unplayable = true;
            Stickers.Add(new HazardousCardSticker());
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