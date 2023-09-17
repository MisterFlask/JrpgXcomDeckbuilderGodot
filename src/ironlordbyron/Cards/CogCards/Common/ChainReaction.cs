using Assets.CodeAssets.Cards.CogCards.Special;
using System.Collections;

namespace Assets.CodeAssets.Cards.CogCards.Common
{
    public class ChainReaction : AbstractCard
    {
        // Deal 8 damage.  Sweeper.  Add a Crusher into your hand.  Cost 1.

        public ChainReaction()
        {
            SetCommonCardAttributes("Chain Reaction", Rarity.RARE, TargetType.ENEMY, CardType.SkillCard, 1);
            AddSticker(new BasicAttackTargetSticker());
            BaseDamage = 8;
            DamageModifiers.Add(new SweeperDamageModifier());
            ProtoSprite = ProtoGameSprite.CogIcon("pendulum-swing");

        }

        public override string DescriptionInner()
        {
            return "Add a Crusher into your hand.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().CreateCardToHand(new Crusher());
        }
    }
}