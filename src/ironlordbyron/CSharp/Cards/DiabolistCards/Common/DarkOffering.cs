using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Common
{
    public class DarkOffering : AbstractCard
    {
        public DarkOffering()
        {
            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            SetCommonCardAttributes("Dark Offering", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 1);
            BaseDefenseValue = 8;
            ProtoSprite = ProtoGameSprite.DiabolistIcon("skull-staff");
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} block to target.  Sacrifice: Gain 1 energy and draw 1 card.";
        }


        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyDefense(target, Owner, BaseDefenseValue);
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