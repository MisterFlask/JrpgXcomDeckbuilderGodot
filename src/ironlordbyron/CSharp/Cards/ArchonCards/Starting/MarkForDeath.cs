using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Starting
{
    public class MarkForDeath : AbstractCard
    {
        public MarkForDeath()
        {
            ProtoSprite = ProtoGameSprite.ArchonIcon("reticule");
            SoldierClassCardPools.Add(typeof(ArchonSoldierClass)); //todo: remove, this is a starting card
            SetCommonCardAttributes(
                "Mark For Death",
                Rarity.BASIC,
                TargetType.ENEMY,
                CardType.AttackCard,
                1
                );

            BaseDamage = 3;
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()}.  Apply 2 Marked.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new MarkedStatusEffect(), 2);
            action().AttackUnitForDamage(target, Owner, BaseDamage, this);
        }
    }
}