using Assets.CodeAssets.Cards.CogCards.Special;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Uncommon
{
    public class AdvancedVivisection : AbstractCard
    {
        // Add two Autocannon Sentries to your hand.  They have Lethal: Gain 2 data points.  Cost 0.
        public AdvancedVivisection()
        {
            ProtoSprite = ProtoGameSprite.CogIcon("split-person-star");

        }

        public override string DescriptionInner()
        {
            return "Add two Autocannon Sentries to your hand.  They have +2 damage and Lethal: Gain 2 data points.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            for(int i = 0; i < 2; i++)
            {
                var newCard = new AutocannonSentry();
                newCard.DamageModifiers.Add(new CruelAnalysisDamageModifier());
            }
        }
    }

    public class CruelAnalysisDamageModifier : DamageModifier
    {
        int Stacks { get; set; } = 0;
        public CruelAnalysisDamageModifier()
        {
            this.CardDescriptionAddendum = "Lethal: Gain 2 data points.";
        }


        public override bool SlayInner(AbstractCard damageSource, AbstractBattleUnit target)
        {

            CardAbilityProcs.GainDataPoints(damageSource, Stacks);
            return true;
        }
        // Lethal: Gain 2 data points.
    }

}