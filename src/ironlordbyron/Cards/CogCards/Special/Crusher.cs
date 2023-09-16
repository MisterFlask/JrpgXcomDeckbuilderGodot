using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Special
{
    public class Crusher : AbstractCard
    {
        public Crusher()
        {
            SetCommonCardAttributes("Crusher", Rarity.NOT_IN_POOL, TargetType.ENEMY, CardType.AttackCard, 2);
            Stickers.Add(new BasicAttackTargetSticker());
            Stickers.Add(new BasicDefendSelfSticker());
            BaseDamage = 15;
            BaseDefenseValue = 8;
            DamageModifiers.Add(new SweeperDamageModifier());
            Stickers.Add(new ExhaustCardSticker());
            ProtoSprite = ProtoGameSprite.CogIcon("daemon-pull");


        }

        public override string DescriptionInner()
        {
            return "";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
        }
    }
}