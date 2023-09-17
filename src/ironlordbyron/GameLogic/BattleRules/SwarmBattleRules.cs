using Assets.CodeAssets.Cards;
using System.Collections;
using System.Linq;

namespace Assets.CodeAssets.GameLogic
{
    public class SwarmBattleRules
    {
        public static void RunEndOfTurnRules(AbstractCard card)
        {
            var numSwarmCardsInHand = GameState.Instance.Deck.Hand.Where(item => item.CardTags.Contains(BattleCardTags.SWARM)).Count();
            ActionManager.Instance.DamageUnitNonAttack(card.Owner, null, numSwarmCardsInHand);
        }

        public static DamageModifier SwarmDamageModifier()
        {
            return new SwarmDamageModifier();
        }
    }

    public class SwarmDamageModifier: DamageModifier
    {
        public override int GetIncrementalDamageAddition(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
        {
            return 1 * BattleHelpers.CardsInHandInOrder().Where(item => item.CardTags.Contains(BattleCardTags.SWARM)).Count();
        }
    }
}