using Assets.CodeAssets.Cards.Stickers;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Special
{
    public class OntologicalWaste : AbstractCard
    {
        // Playable for 1.  Retained: A random character takes 3 Stress.
        public OntologicalWaste()
        {
            SetCommonCardAttributes("Ontological Waste", Rarity.NOT_IN_POOL, TargetType.NO_TARGET_OR_SELF, CardType.ConditionCard, 2);
            this.Stickers.Add(new ExhaustCardSticker());
            this.Stickers.Add(new HazardousCardSticker
            {
                Stacks = 2
            });
            ProtoSprite = ProtoGameSprite.CogIcon("ontological-waste");

        }

        public override string DescriptionInner()
        {
            return "";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            
        }

        public override void InHandAtEndOfTurnAction()
        {
        }
    }
}