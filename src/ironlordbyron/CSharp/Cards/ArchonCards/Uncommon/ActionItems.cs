using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using System.Linq;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Uncommon
{
    public class ActionItems : AbstractCard
    {
        // Two random cards in your hand cost 1 less for the rest of combat.  Cost 1.  Exhaust.

        public ActionItems()
        {
            SetCommonCardAttributes("Action Items", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1, typeof(ArchonSoldierClass),

                protoGameSprite: ProtoGameSprite.ArchonIcon("checklist"));
            Stickers.Add(new ExhaustCardSticker());
        }

        public override string DescriptionInner()
        {
            return $"Two random cards in your hand cost 1 less for the rest of combat.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            var twoOtherRandomCards = state().Deck.Hand.Where(item => item != this).Shuffle().TakeUpTo(2);
            foreach (var card in twoOtherRandomCards)
            {
                card.PersistentCostModifiers.Add(new RestOfCombatCostModifier(-1));
            }
        }
    }
}