using Assets.CodeAssets.Cards.Stickers;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Special
{
    public class AutocannonSentry : AbstractCard
    {

        public AutocannonSentry()
        {
            SetCommonCardAttributes("Autocannon Sentry", Rarity.NOT_IN_POOL, TargetType.ENEMY, CardType.AttackCard, 1);
            BaseDamage = 4;
            DamageModifiers.Add(new PrecisionDamageModifier());
            AddSticker(new LightCardSticker());
            ProtoSprite = ProtoGameSprite.CogIcon("autogun");

        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} to the target, twice.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            Action_AttackTarget(target);
        }
    }
}