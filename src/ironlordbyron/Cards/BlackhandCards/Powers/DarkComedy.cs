using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Powers
{
    public class DarkComedy : AbstractCard
    {
        // Power: At the beginning of your turn, draw cards equal to the number of Burning characters.

        public DarkComedy()
        {
            SetCommonCardAttributes("Dark Comedy", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 3);
            ProtoSprite = ProtoGameSprite.BlackhandIcon("balloon-dog");
        }

        public override string DescriptionInner()
        {
            return $"At the beginning of your turn, draw cards equal to the number of Burning characters.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new DarkComedyStatusEffect());
            this.Action_Exhaust();
        }
    }

    public class DarkComedyStatusEffect : AbstractStatusEffect
    {
        public DarkComedyStatusEffect()
        {
            Name = "Dark Comedy";
        }

        public override string Description => $"At the beginning of your turn, draw cards equal to the number of Burning characters times {DisplayedStacks()}.";
    }
}