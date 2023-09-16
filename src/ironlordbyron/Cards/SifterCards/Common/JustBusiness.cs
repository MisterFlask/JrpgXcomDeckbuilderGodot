using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.SifterCards.Common
{
    public class JustBusiness : AbstractCard
    {
        // Deal 8 damage.  Precision.
        
        public JustBusiness()
        {
            SetCommonCardAttributes("Just Business", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 1);
            this.DamageModifiers.Add(new PrecisionDamageModifier());

            this.ProtoSprite =
                ProtoGameSprite.ArchonIcon("half-crawling-body");
        }

        public override string DescriptionInner()
        {
            return $"Deal 8 damage."; // precision added by modifier
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
        }
    }
}