using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Common
{
    // cost 0.  Lose 3 stress.  Exhaust.
    public class Wail : AbstractCard
    {
        public Wail()
        {
            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            SetCommonCardAttributes("Wail", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);

            ProtoSprite = ProtoGameSprite.DiabolistIcon("terror");
        }

        public override string DescriptionInner()
        {
            return $"Relieve 3 stress from {ownerDisplayString()}.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(Owner, new StressStatusEffect(), -3);
            Action_Exhaust();
        }
    }
}