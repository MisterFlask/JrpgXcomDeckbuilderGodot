using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.BlackhandCards.Skills
{
    public class WalkingGenerator : AbstractCard
    {
        // Apply 12 block to target ally.  Charged: Refund 1 energy.  cost 1.

        public WalkingGenerator()
        {
            SetCommonCardAttributes("Walking Generator", Rarity.RARE, TargetType.ALLY, CardType.SkillCard, 1, typeof(BlackhandSoldierClass));
            ProtoSprite = ProtoGameSprite.BlackhandIcon("power-generator");
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} block to target ally.  Discharge: Refund 1 energy.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyDefense(target, Owner, BaseDefenseValue);
            CardAbilityProcs.Discharge(this, () =>
            {
                state().energy++;
            });
        }
    }
}