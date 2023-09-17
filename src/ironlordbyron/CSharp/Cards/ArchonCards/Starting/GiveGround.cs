using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Starting
{
    public class GiveGround : AbstractCard
    {
        public GiveGround()
        {
            ProtoSprite = ProtoGameSprite.ArchonIcon("ringmaster");

            SoldierClassCardPools.Add(typeof(ArchonSoldierClass)); // todo: remove
            SetCommonCardAttributes(
                "Fall back, damn you!",
                Rarity.BASIC,
                TargetType.ALLY,
                CardType.SkillCard,
                1
                );

            BaseDefenseValue = 5;
        }

        public override string DescriptionInner()
        {
            return $"Remove Advanced from an ally.  Apply {DisplayedDefense()} defense.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().RemoveStatusEffect<AdvancedStatusEffect>(target);
            action().ApplyDefense(target, Owner, BaseDefenseValue);
        }
    }
}