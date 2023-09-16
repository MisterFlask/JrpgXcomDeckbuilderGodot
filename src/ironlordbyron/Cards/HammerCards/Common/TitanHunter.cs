using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards.Common
{
    public class TitanHunter : AbstractCard
    {
        // Gain 1 strength.  Deal 10 damage.  Cost 2.

        public TitanHunter()
        {
            SetCommonCardAttributes("Titan Hunter", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 2);
            BaseDamage = 10;
            Stickers.Add(new ExertCardSticker());
            DamageModifiers.Add(new SlayerDamageModifier());
            ProtoSprite = ProtoGameSprite.HammerIcon("atlas");
        }

        public override string DescriptionInner()
        {
            return $"Deal 10 damage.  Gain 1 strength.  Slayer.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            Action_ApplyStatusEffectToOwner(new StrengthStatusEffect(), 1);
        }
    }
}