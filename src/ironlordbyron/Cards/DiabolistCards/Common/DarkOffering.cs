using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.GameLogic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Common
{
    public class DarkOffering : AbstractCard
    {
        public DarkOffering()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Dark Offering", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 1);
            this.BaseDefenseValue = 8;
            ProtoSprite = ProtoGameSprite.DiabolistIcon("skull-staff");
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} block to target.  Sacrifice: Gain 1 energy and draw 1 card.";
        }


        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyDefense(target, this.Owner, BaseDefenseValue);
            this.Sacrifice(() =>
            {
                action().PushActionToBack("DarkOffering_OnPlay", () =>
                {
                    state().energy++;
                });
                action().DrawCards(1);
            });
        }


    }
}