namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Special
{
    public class Manuever : AbstractCard
    {

        public Manuever()
        {
            SetCommonCardAttributes("Manuever", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 0);
        }

        public override string DescriptionInner()
        {
            return "If targeted ally has Advanced, remove it.  Otherwise, apply it.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            if (!target.HasStatusEffect<AdvancedStatusEffect>())
            {
                action().ApplyStatusEffect(target, new AdvancedStatusEffect());
            }
            else
            {
                action().RemoveStatusEffect<AdvancedStatusEffect>(target);
            }
            Action_Exhaust();
        }
    }
}