using System.Collections;

namespace Assets.CodeAssets.Cards.SifterCards.Common
{
    public class LootCrate : AbstractCard
    {
        // Does nothing.  Gilded 5.
        public LootCrate()
        {
            SetCommonCardAttributes("Loot Crate", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 10);
            Stickers.Add(new GildedCardSticker(5));

            this.ProtoSprite =
                ProtoGameSprite.ArchonIcon("wooden-crate");
        }

        public override string DescriptionInner()
        {
            return "Does nothing.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
        }
    }
}