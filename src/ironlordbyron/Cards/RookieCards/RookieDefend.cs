using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.RookieCards
{
    public class RookieDefend : Defend
    {
        public RookieDefend()
        {
            ProtoSprite = ProtoGameSprite.RookieIcon("attached-shield");
        }
    }
}