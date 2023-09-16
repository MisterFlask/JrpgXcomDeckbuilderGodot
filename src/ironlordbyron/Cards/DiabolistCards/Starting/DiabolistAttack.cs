using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Starting
{
    public class DiabolistAttack : Gunfire
    {
        public DiabolistAttack()
        {
            ProtoSprite = ProtoGameSprite.DiabolistIcon("gunshot");
        }
    }
}