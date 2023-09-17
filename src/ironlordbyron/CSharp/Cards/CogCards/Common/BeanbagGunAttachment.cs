using GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic.BattleRules;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.CogCards.Common
{
    public class BeanbagGunAttachment : AbstractCard
    {
        public BeanbagGunAttachment()
        {
            SetCommonCardAttributes("Beanbag Gun Attachment", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
            ProtoSprite = ProtoGameSprite.CogIcon("coffee-beans");
        }

        // The leftmost attack in your hand gains "Whenever this deals damage to an enemy, apply 1 Weak to it."
        public override string DescriptionInner()
        {
            return "The leftmost attack in your hand gains 'whenever this deals damage to an enemy, apply 1 weak to it.'";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            var leftmostAttack = BattleHelpers.LeftmostCardInHandThat(item => item.CardType == CardType.AttackCard);
            if (leftmostAttack != null)
            {
                leftmostAttack.DamageModifiers.Add(new AppliesWeakDamageModifier());
            }
        }
    }


    public class AppliesWeakDamageModifier : DamageModifier
    {
        public AppliesWeakDamageModifier()
        {
            CardDescriptionAddendum = "Whenever this deals damage to an enemy, apply 1 Weak to it.";
        }

        public override void OnStrike(AbstractCard damageSource, AbstractBattleUnit target, int totalDamageAfterModifiers)
        {
            action().ApplyStatusEffect(target, new WeakenedStatusEffect());
        }
    }
}