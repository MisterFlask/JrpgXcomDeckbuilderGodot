using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Attacks
{
    public class FromHell : AbstractCard
    {
        public FromHell()
        {
            SetCommonCardAttributes("From Hell", Rarity.UNCOMMON, TargetType.ENEMY, CardType.AttackCard, 2);
            this.BaseDamage = 10;
            ProtoSprite = ProtoGameSprite.BlackhandIcon("flaming-trident");

        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage.  Inferno: This card deals 8 more damage for the rest of combat.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().AttackWithCard(this, target);
            action().PushActionToBack("FromHell_OnPlay", () =>
            {
                this.BaseDamage += 8;
            });
        }

    }
}