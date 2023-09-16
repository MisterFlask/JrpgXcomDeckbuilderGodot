using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Starting
{
    public class DiabolistDefend : Defend
    {
        public DiabolistDefend()
        {
            ProtoSprite = ProtoGameSprite.DiabolistIcon("shield-reflect");
        }
    }
}