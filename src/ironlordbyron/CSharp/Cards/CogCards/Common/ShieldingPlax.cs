using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.Stickers;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.CogCards.Common
{
    public class ShieldingPlax : AbstractCard
    {
        public ShieldingPlax()
        {
            SetCommonCardAttributes("Shielding Plax", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 1);
            BaseDefenseValue = 4;
            ProtoSprite = ProtoGameSprite.CogIcon("honeypot");
        }

        // Apply 6 block and 1 Charged to target ally.
        // Discard all cards with Hazardous.
        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} block and 1 Charged to target ally.  Discard all cards with Hazardous.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyDefenseToTarget(target);
            foreach (var card in state().Deck.Hand)
            {
                if (card.HasSticker<HazardousCardSticker>())
                {
                    action().DiscardCard(card);
                }
            }
        }
    }
}