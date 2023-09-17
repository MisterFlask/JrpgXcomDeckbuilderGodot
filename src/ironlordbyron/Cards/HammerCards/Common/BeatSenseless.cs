using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;

namespace Assets.CodeAssets.Cards.HammerCards.Common
{
    public class BeatSenseless : AbstractCard
    {
        // Deal 10 damage and 2 Vulnerable.  Cost 2.
        // Brute: Gain 1 energy.
        public BeatSenseless()
        {
            this.SoldierClassCardPools.Add(typeof(HammerSoldierClass));

            SetCommonCardAttributes("Beat Senseless", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 2);
            BaseDamage = 10;
            ProtoSprite = ProtoGameSprite.HammerIcon("thor-hammer");
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage and 2 Vulnerable.  Exert.  Brute: Gain 1 energy.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            Action_ApplyStatusEffectToTarget(new VulnerableStatusEffect(), 2, target);
            CardAbilityProcs.Brute(this, () =>
            {
                CardAbilityProcs.Refund(this, 1);
            });
        }
    }
}