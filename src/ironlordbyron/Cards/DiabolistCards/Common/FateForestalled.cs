using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Common
{
    public class FateForestalled : AbstractCard
    {
        // grant 11 defense.  Cost 2.  If a Swarm is in your hand, grant 3 temporary HP.

        public FateForestalled()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Fate Forestalled", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 2);
            this.BaseDefenseValue = 12;
            ProtoSprite = ProtoGameSprite.DiabolistIcon("templar-shield");
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} block to target.  If a Swarm is in your hand, grant 3 temporary HP.";
        }


        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyDefense(target, this.Owner, BaseDefenseValue);
            if (state().Deck.Hand.Any(item => item.CardTags.Contains(BattleCardTags.SWARM))){
                action().ApplyStatusEffect(target, new TemporaryHpStatusEffect(), 3);
            }
        }

    }
}