using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using System.Collections;

namespace Assets.CodeAssets.Cards.ArchonCards.Starting
{
    public class MarkForDeath : AbstractCard
    {
        public MarkForDeath()
        {
            ProtoSprite = ProtoGameSprite.ArchonIcon("reticule");
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass)); //todo: remove, this is a starting card
            this.SetCommonCardAttributes(
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