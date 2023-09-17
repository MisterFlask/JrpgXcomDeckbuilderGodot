using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using System.Linq;

namespace Assets.CodeAssets.Cards.ArchonCards.Uncommon
{
    public class CommandingTone : AbstractCard
    {
        public CommandingTone()
        {
            SetCommonCardAttributes("Commanding Tone", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1, typeof(ArchonSoldierClass),
                protoGameSprite: ProtoGameSprite.ArchonIcon("shouting"));
        }

        public override string DescriptionInner()
        {
            return $"Add a random attack card from your draw pile into your hand; it costs 1 less this turn.  For the rest of combat, it gains 4 damage.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            var attackCardFromDrawPile = state().Deck.DrawPile.Where(item => item.CardType == CardType.AttackCard).FirstOrDefault();
            if (attackCardFromDrawPile != null)
            {
                attackCardFromDrawPile.RestOfTurnCostMod--;
                attackCardFromDrawPile.BaseDamage += 4;
            }
        }
    }
}