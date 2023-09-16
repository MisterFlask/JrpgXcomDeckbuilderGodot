using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Uncommon
{
    public class HandCannon : AbstractCard
    {
        // Deal 20 damage.  Buster.  Cost 2.

        public HandCannon()
        {
            SetCommonCardAttributes("Hand Cannon", Rarity.UNCOMMON, TargetType.ENEMY, CardType.AttackCard, 2,
                protoGameSprite: ProtoGameSprite.ArchonIcon("cannon-shot"));
            Stickers.Add(new BasicAttackTargetSticker());
            BaseDamage = 20;
            DamageModifiers.Add(new BusterDamageModifier());
        }

        public override string DescriptionInner()
        {
            return $"";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            
        }
    }
}