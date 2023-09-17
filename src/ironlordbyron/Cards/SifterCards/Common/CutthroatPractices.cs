using System.Collections;

namespace Assets.CodeAssets.Cards.SifterCards.Common
{
    public class CutthroatPractices : AbstractCard
    {
        // Deal 8 damage.  Ambush: Then deal another 8 damage.

        public CutthroatPractices()
        {
            SetCommonCardAttributes("Cutthroat Practices", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 1);
            BaseDamage = 8;

            this.ProtoSprite =
                ProtoGameSprite.ArchonIcon("tearing");
        }
        public override string DescriptionInner()
        {
            return $"Dead {DisplayedDamage()} damage.  Ambush: Then do it again.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            CardAbilityProcs.Ambush(this, () =>
            {
                Action_AttackTarget(target);
            });
        }
    }
}