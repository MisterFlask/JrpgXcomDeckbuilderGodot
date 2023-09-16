using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.Stickers
{
    public class CommonCardStickers : MonoBehaviour
    {

    }

    public class HazardousCardSticker: AbstractCardSticker
    {
        public HazardousCardSticker()
        {
            
        }

        public override string GetCardTooltipIfAny()
        {
            return $"At end of turn, a random ally takes {Stacks} damage.";
        }

        public override string CardDescriptionAddendum()
        {
            return $"Hazardous {Stacks}";
        }

        public int Stacks { get; set; } = 2;

        public override void OnEndOfTurnWhileInHand(AbstractCard card)
        {
            ActionManager.Instance.DamageUnitNonAttack(GameState.Instance.AllyUnitsInBattle.PickRandom(), null, Stacks);
        }
    }


    public class LightCardSticker: AbstractCardSticker
    {
        bool usedThisTurn = false;

        public override string GetCardTooltipIfAny()
        {
            return $"The first time you draw this card in a turn, draw a card.";
        }

        public override string CardDescriptionAddendum()
        {
            return $"Light";
        }

        public override void OnCardDrawn(AbstractCard card)
        {
            if (!usedThisTurn)
            {
                ActionManager.Instance.DrawCards(1);
            }
            usedThisTurn = true;
        }

        public override void OnTurnStart(AbstractCard card)
        {
            usedThisTurn = false;
        }
    }

}