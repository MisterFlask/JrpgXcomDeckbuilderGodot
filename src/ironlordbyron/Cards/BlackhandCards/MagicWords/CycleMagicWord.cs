using Assets.CodeAssets.GameLogic;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.MagicWords
{
    public class CycleMagicWord : MagicWord
    {

        public override string MagicWordTitle => "Cycle";

        public override string MagicWordDescription => "Discard this card, then draw a card.";
    }
}