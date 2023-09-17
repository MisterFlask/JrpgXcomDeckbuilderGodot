namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.SifterCards.Rare
{
    public class Stonks : AbstractCard
    {
        //   Deal 10 damage.  Lethal: Increase this card's Hoard value by 2 PERMANENTLY.   Hoard 2.

        public Stonks()
        {
            SetCommonCardAttributes("Stonks!", Rarity.RARE, TargetType.ENEMY, CardType.AttackCard, 1);
            DamageModifiers.Add(new StonksDamageModifier());
            Stickers.Add(new BasicAttackTargetSticker());
            Stickers.Add(new GildedCardSticker(2));
            BaseDamage = 10;
            ProtoSprite = ProtoGameSprite.CogIcon("chart");

        }

        public override string DescriptionInner()
        {
            return "Lethal: Increase this card's Hoard value by 2 PERMANENTLY.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {

        }
    }

    public class StonksDamageModifier : DamageModifier
    {
        public override bool SlayInner(AbstractCard damageSource, AbstractBattleUnit target)
        {
            var gildedCard = damageSource.CorrespondingPermanentCard().GetStickerOfType<GildedCardSticker>();
            if (gildedCard != null)
            {
                gildedCard.GildedValue += 2;
            }
            return true;
        }
    }
}