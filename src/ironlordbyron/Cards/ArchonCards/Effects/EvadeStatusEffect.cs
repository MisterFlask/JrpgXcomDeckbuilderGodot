using System.Collections;

namespace Assets.CodeAssets.Cards.ArchonCards.Effects
{
    public class EvadeStatusEffect : AbstractStatusEffect
    {
        public EvadeStatusEffect()
        {
            Name = "Evade";
        }
        public override string Description => "Receive 33% less damage.";

        public override float DefenseReceivedIncrementalMultiplier()
        {
            return -.33f;
        }
    }
}