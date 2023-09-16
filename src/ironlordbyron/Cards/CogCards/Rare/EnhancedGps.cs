using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Rare
{
    public class EnhancedGps : AbstractCard
    {
        // increases dexterity by 3.  Cost 1.  Exhaust.

        public EnhancedGps()
        {
            SetCommonCardAttributes("Very Enhanced GPS", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
            ProtoSprite = ProtoGameSprite.CogIcon("gps");
        }

        public override string DescriptionInner()
        {
            return "Increase Dexterity by 3.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToOwner(new DexterityStatusEffect(), 3);
        }
    }
}