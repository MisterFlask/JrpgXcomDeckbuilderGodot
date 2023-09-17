using System.Collections;

namespace Assets.CodeAssets.Cards.BlackhandCards.Attacks
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
            CardAbilityProcs.Inferno(this, () =>
            {
                action().DrawCards(1);
            });
            CardAbilityProcs.Ambush(this, () =>
            {
                action().AttackWithCard(this, target);
            });
            
        }
    }
}