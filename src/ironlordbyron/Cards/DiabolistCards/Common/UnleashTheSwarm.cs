using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;

namespace Assets.CodeAssets.Cards.DiabolistCards.Common
{
    public class UnleashTheSwarm : AbstractCard
    {
        // cost 3.
        // Create two copies of Hellish Swarm in your draw pile.  Deal 15 damage.  Anti-Personnel.  Swarm.

        public UnleashTheSwarm()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Unleash The Swarm", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 3);
            this.CardTags.Add(BattleCardTags.SWARM);
            this.BaseDamage = 4;
            ProtoSprite = ProtoGameSprite.DiabolistIcon("ants");

        }

        // Deal 4 damage and apply 1 Vulnerable.  Draw a card.  Exhaust.  Cost 0.
        // Swarm.
        public override string DescriptionInner()
        {
            return "Deal 4 damage and apply 1 Vulnerable.  Draw a card.  Exhaust.  Cost 0.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new VulnerableStatusEffect(), 1);
            action().DrawCards(1);
            this.Action_Exhaust();
        }
    }
}