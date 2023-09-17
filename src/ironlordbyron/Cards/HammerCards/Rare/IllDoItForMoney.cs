using System.Collections;
using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
namespace Assets.CodeAssets.Cards.HammerCards.Rare
{
    public class IllDoItForMoney : AbstractCard
    {
        public IllDoItForMoney()
        {
            this.SoldierClassCardPools.Add(typeof(HammerSoldierClass));

            SetCommonCardAttributes("I'll Do It For Money", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
            ProtoSprite = ProtoGameSprite.HammerIcon("receive-money");

        }

        // Lose 5 credits and 20 stress.  Exhaust.  Cost 0.
        public override string DescriptionInner()
        {
            return "Lose 5 credits and relieve 20 stress.  Exhaust.  Discarded:  Draw a card.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            if (state().Credits < 5)
            {
                action().Shout(this.Owner, "Cheap bastard.");
                action().ApplyStress(this.Owner, 1);
                Action_Exhaust();

                return;
            }
            state().Credits -= 5;
            Action_Exhaust();

        }

        public override void OnManualDiscard()
        {
            action().DrawCards(1);
        }
    }
}