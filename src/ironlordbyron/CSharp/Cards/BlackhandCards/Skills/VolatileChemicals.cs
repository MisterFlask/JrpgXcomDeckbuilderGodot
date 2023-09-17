namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.BlackhandCards.Skills
{
    public class VolatileChemicals : AbstractCard
    {
        public VolatileChemicals()
        {
            SetCommonCardAttributes("Volatile Chemicals", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);

            ProtoSprite = ProtoGameSprite.BlackhandIcon("chemical-tank");
        }

        public override string DescriptionInner()
        {
            return "Sacrifice: Gain 2 Energy.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            this.Sacrifice(() =>
            {
                action().PushActionToBack("VolatileChemicals_OnPlay", () =>
                {
                    state().energy += 2;
                });
            });
        }
    }
}