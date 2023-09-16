using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards.SifterCards.Rare
{
    public class FinancialAdvisor : AbstractCard
    {
        // deal 20 damage.  Slay: A random card in an ally's deck gains Hoard 2 PERMANENTLY.  Cost 2.

        public FinancialAdvisor()
        {
            SetCommonCardAttributes("Financial Advisor", Rarity.RARE, TargetType.ENEMY, CardType.AttackCard, 2);
            DamageModifiers.Add(new FinancialAdvisorSlayTrigger());
            ProtoSprite = ProtoGameSprite.CogIcon("shadow-follower");

        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} to target.  Lethal: A random card in an ally's deck gains Hoard 2 PERMANENTLY.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
        }
    }
}
public class FinancialAdvisorSlayTrigger: DamageModifier
{
    public override bool SlayInner(AbstractCard damageSource, AbstractBattleUnit target)
    {
        var alliesNotMe = state().AllyUnitsInBattle.Where(item => item != damageSource.Owner && !item.IsDead);
        var randomCard = alliesNotMe.SelectMany(item => item.CardsInPersistentDeck).Shuffle().FirstOrDefault();
        if (randomCard != null)
        {
            randomCard.Stickers.Add(new GildedCardSticker(2));
        }
        return true;
    }
}