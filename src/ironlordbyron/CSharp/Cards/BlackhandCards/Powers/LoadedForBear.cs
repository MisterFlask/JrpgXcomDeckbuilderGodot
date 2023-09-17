namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.BlackhandCards.Powers
{
    public class LoadedForBear : AbstractCard
    {
        // Whenever you play a Grenade card, play it again.

        public LoadedForBear()
        {
            SetCommonCardAttributes("Loaded for Bear", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1);
            ProtoSprite = ProtoGameSprite.BlackhandIcon("bear-head");
        }

        public override string DescriptionInner()
        {
            return "Whenever you play a grenade card, play it again.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(Owner, new LoadedForBearStatusEffect(), 1);
        }
    }

    public class LoadedForBearStatusEffect : AbstractStatusEffect
    {
        public LoadedForBearStatusEffect()
        {
            Name = "Loaded for Bear";
        }

        public override string Description => $"Whenever you play a grenade card, copy it {DisplayedStacks()} times";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool ownedByMe)
        {
            if (cardPlayed.NameContains("grenade"))
            {
                for (int i = 0; i < Stacks; i++)
                {
                    cardPlayed.EvokeCardEffect(targetOfCard);
                }
            }
        }
    }
}