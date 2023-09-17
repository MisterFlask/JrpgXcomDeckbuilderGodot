using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections;

namespace Assets.CodeAssets.Cards.CogCards.Common
{
    public class ExternalBatteryPack : AbstractCard
    {
        // apply 3 Charged to an ally.

        public ExternalBatteryPack()
        {
            SetCommonCardAttributes("External Battery Pack", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 1);
            ProtoSprite = ProtoGameSprite.CogIcon("battery-pack-alt");
        }

        public override string DescriptionInner()
        {
            return "Apply 3 Charged to an ally.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new ChargedStatusEffect(), 3);
        }
    }
}