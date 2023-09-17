using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.BlackhandCards.Powers
{
    public class ArmamentsRequisition : AbstractCard
    {
        // At the beginning of each turn, put a random grenade into your discard pile.
        public ArmamentsRequisition()
        {
            SetCommonCardAttributes("Armaments Requisition",
                Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1);
            ProtoSprite = ProtoGameSprite.BlackhandIcon("fireball");
        }

        public override string DescriptionInner()
        {
            return "At the beginning of each turn, add a grenade into your discard pile.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(Owner, new GuerillaMindsetStatusEffect(), 1);
        }
    }

    public class ArmamentsRequisitionStatusEffect : AbstractStatusEffect
    {
        public ArmamentsRequisitionStatusEffect()
        {
            Name = "Armaments Requisition";
        }

        public override string Description => $"At the end of each turn, add {DisplayedStacks()} grenades onto your draw pile.";

        public override void OnTurnEnd()
        {
            for (int i = 0; i < Stacks; i++)
            {
                var grenade = GetRandomGrenadeCard();
                action().CreateCardToBattleDeckDrawPile(grenade, CardCreationLocation.TOP);
            }
        }

        private AbstractCard GetRandomGrenadeCard()
        {
            return new List<AbstractCard>
            {
                new SmogGrenade(),
                new NapalmGrenade(),
                new FlashbangGrenade()
            }.PickRandom();
        }
    }
}