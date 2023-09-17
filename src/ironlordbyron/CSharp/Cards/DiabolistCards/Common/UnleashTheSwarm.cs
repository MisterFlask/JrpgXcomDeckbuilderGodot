using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Common
{
    public class UnleashTheSwarm : AbstractCard
    {
        // cost 3.
        // Create two copies of Hellish Swarm in your draw pile.  Deal 15 damage.  Anti-Personnel.  Swarm.

        public UnleashTheSwarm()
        {
            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            SetCommonCardAttributes("Unleash The Swarm", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 3);
            CardTags.Add(BattleCardTags.SWARM);
            BaseDamage = 4;
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
            Action_Exhaust();
        }
    }
}