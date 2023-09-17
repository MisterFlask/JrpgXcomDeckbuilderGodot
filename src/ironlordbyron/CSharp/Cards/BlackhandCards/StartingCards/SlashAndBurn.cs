namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.BlackhandCards.StartingCards
{
    public class SlashAndBurn : AbstractCard
    {
        public SlashAndBurn()
        {
            BaseDamage = 4;
            ProtoSprite = ProtoGameSprite.BlackhandIcon("regeneration");
            SetCommonCardAttributes("Slash And Burn", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 0);
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage.  Inferno: Draw a card.  Ambush: Then deal another {DisplayedDamage()}.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().AttackWithCard(this, target);
            this.Inferno(() =>
            {
                action().DrawCards(1);
            });
            this.Ambush(() =>
            {
                action().AttackWithCard(this, target);
            });

        }
    }
}