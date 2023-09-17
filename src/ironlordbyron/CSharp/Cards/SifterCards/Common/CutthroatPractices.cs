namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.SifterCards.Common
{
    public class CutthroatPractices : AbstractCard
    {
        // Deal 8 damage.  Ambush: Then deal another 8 damage.

        public CutthroatPractices()
        {
            SetCommonCardAttributes("Cutthroat Practices", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 1);
            BaseDamage = 8;

            ProtoSprite =
                ProtoGameSprite.ArchonIcon("tearing");
        }
        public override string DescriptionInner()
        {
            return $"Dead {DisplayedDamage()} damage.  Ambush: Then do it again.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            this.Ambush(() =>
            {
                Action_AttackTarget(target);
            });
        }
    }
}