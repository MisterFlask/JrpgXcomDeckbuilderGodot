using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Effects
{
    public class LogisticalSupportStatusEffect : AbstractStatusEffect
    {

        public static string NAME = "Logistics";

        public LogisticalSupportStatusEffect()
        {
            this.Name = NAME;
            this.StatusLocalityType = StatusLocalityType.GLOBAL;
            this.StatusPolarityType = StatusPolarityType.BUFF;
        }

        public override string Description => $"The next {DisplayedStacks()} times that you play a card, play it again.";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit target, bool ownedByMe)
        {
            action().EvokeCardEffect(cardPlayed, target);

        }
    }
}