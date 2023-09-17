using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects;
using System.Linq;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Uncommon
{
    public class HeadcountReduction : AbstractCard
    {
        //  Apply 3 Mark to an enemy.  Add the Bounty characteristic to a random attack in your hand. Exhausted: deal 10 damage to ALL enemies.  Cost 0.

        public HeadcountReduction()
        {
            SetCommonCardAttributes("HeadcountReduction", Rarity.UNCOMMON, TargetType.ENEMY, CardType.SkillCard, 0,
                protoGameSprite: ProtoGameSprite.ArchonIcon("headshot"));
        }

        public override string DescriptionInner()
        {
            return $"Apply 3 Mark to an enemy.  Add the Bounty characteristic to a random attack in your hand.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToTarget(new MarkedStatusEffect(), 3, target);

            var randomCardInHand = state().Deck.Hand.Where(item => item.CardType == CardType.AttackCard).PickRandom();
            randomCardInHand.DamageModifiers.Add(new BountyDamageModifier());
        }
    }
}