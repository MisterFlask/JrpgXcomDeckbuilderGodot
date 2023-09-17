namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.CogCards.Rare
{
    public class PeakPerformance : AbstractCard
    {
        // add two Crushers to your hand.  They gain 10 attack and 10 defense.

        public PeakPerformance()
        {
            SetCommonCardAttributes("Peak Performance", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
            ProtoSprite = ProtoGameSprite.CogIcon("peaks");
        }

        public override string DescriptionInner()
        {
            return "add two Crushers to your hand.  They gain 10 attack and 10 defense.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            for (int i = 0; i < 2; i++)
            {
                var newCard = new Crusher();
                newCard.BaseDamage += 10;
                newCard.BaseDefenseValue += 10;
                action().CreateCardToHand(newCard);
            }
        }
    }
}