using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.RookieCards
{
    public class RookieAttack : Gunfire
    {
        public RookieAttack()
        {
            ProtoSprite = ProtoGameSprite.RookieIcon("gunshot");
        }
    }
}