using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.StatusEffects;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.HammerCards.Common
{
    public class PowerArmor : AbstractCard
    {
        // Gain 3 Charged.  Cost 1.

        public PowerArmor()
        {
            SetCommonCardAttributes("Power Armor", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
            ProtoSprite = ProtoGameSprite.HammerIcon("armor-head");

        }

        public override string DescriptionInner()
        {
            return $"Gain 3 Charged.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToOwner(new ChargedStatusEffect(), 3);
        }
    }
}