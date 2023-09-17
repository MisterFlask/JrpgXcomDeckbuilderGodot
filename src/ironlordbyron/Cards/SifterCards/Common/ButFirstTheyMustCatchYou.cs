using System.Collections;
using System.Linq;

namespace Assets.CodeAssets.Cards.SifterCards.Common
{
    public class ButFirstTheyMustCatchYou : AbstractCard
    {
        // apply 6 defense.  Applies 5 more defense for EACH attack with "Buster" in your deck.
        
        public ButFirstTheyMustCatchYou()
        {
            SetCommonCardAttributes("But First They Must Catch You", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 1);

            this.ProtoSprite =
                ProtoGameSprite.ArchonIcon("run");
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} defense.  Applies 5 more defense for EACH attack card with 'Buster' in your draw, hand and discard piles.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            var busterCardsInDeck= state().Deck.DrawHandAndDiscardPiles
                .Where(item => item.GetDamageModifierOfType<BusterDamageModifier>() != null).Count();
            action().ApplyDefense(target, this.Owner, 6 + 5 * busterCardsInDeck);
        }
    }
}