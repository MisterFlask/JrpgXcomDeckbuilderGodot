namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.SifterCards.Common
{
    public class GildedGun : AbstractCard
    {
        GildedCardSticker sticker = new GildedCardSticker(2);

        public GildedGun()
        {
            // deal 6 damage.  Precision.  Hoard 2.  Cost 0.  Gets an additional +2 Gilded for EACH Gilded Gun in your deck.
            SetCommonCardAttributes("Gilded Gun", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 0);
            Stickers.Add(sticker);
            DamageModifiers.Add(new PrecisionDamageModifier());

            ProtoSprite =
                ProtoGameSprite.ArchonIcon("machine-gun");
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
        }

        public override void OnStartup()
        {
            foreach (var card in state().Deck.TotalDeckList)
            {
                if (card.GetType() != GetType())
                {
                    continue;
                }
                sticker.GildedValue += 2;
            }
        }
    }
}