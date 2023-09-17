using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects;
using System.Linq;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Common
{
    public class FateForestalled : AbstractCard
    {
        // grant 11 defense.  Cost 2.  If a Swarm is in your hand, grant 3 temporary HP.

        public FateForestalled()
        {
            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            SetCommonCardAttributes("Fate Forestalled", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 2);
            BaseDefenseValue = 12;
            ProtoSprite = ProtoGameSprite.DiabolistIcon("templar-shield");
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} block to target.  If a Swarm is in your hand, grant 3 temporary HP.";
        }


        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyDefense(target, Owner, BaseDefenseValue);
            if (state().Deck.Hand.Any(item => item.CardTags.Contains(BattleCardTags.SWARM)))
            {
                action().ApplyStatusEffect(target, new TemporaryHpStatusEffect(), 3);
            }
        }

    }
}