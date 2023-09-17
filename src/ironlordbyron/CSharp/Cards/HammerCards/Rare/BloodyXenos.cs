namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.HammerCards.Rare
{
    public class BloodyXenos : AbstractCard
    {
        // B___dy Xenos

        // Gain 30 Temporary HP.  Gain 10 stress.  Exhaust.  Cost 1.
        public BloodyXenos()
        {
            SetCommonCardAttributes("B___dy Xenos", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
            ProtoSprite = ProtoGameSprite.HammerIcon("alien-bug");
        }

        public override string DescriptionInner()
        {
            return $"Gain 30 temporary HP.  Gain 10 stress.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToOwner(new TemporaryHpStatusEffect(), 30);
            action().ApplyStress(Owner, 10);
            Action_Exhaust();
        }
    }
}