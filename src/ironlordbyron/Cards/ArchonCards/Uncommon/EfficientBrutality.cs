using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Uncommon
{
    public class EfficientBrutality : AbstractCard
    {
        public EfficientBrutality()
        {
            SetCommonCardAttributes("Efficient Brutality", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.AttackCard, 2,
                protoGameSprite: ProtoGameSprite.ArchonIcon("bloody-stash"));
        }

        public override string DescriptionInner()
        {
            return $"Deal 12 damage.  Apply 2 strength and 8 stress to all OTHER allies.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            //todo
        }
    }
}