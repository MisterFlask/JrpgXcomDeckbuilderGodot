using System.Collections;

namespace Assets.CodeAssets.Cards.CogCards.Rare
{
    public class SafetyDrive : AbstractCard
    {
        // Add a shield drone to your hand.  The leftmost attack card in your hand gains "Then, add a Shield Drone to your hand.";

        public SafetyDrive()
        {
            SetCommonCardAttributes("Safety Drive", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);

        }

        public override string DescriptionInner()
        {
            return "todo";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            throw new System.NotImplementedException();
        }
    }



}