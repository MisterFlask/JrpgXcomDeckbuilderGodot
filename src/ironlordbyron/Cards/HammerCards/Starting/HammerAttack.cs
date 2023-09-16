using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Starting
{
    public class HammerAttack : Gunfire
    {
        public HammerAttack()
        {
            ProtoSprite = ProtoGameSprite.HammerIcon("thor-hammer");
        }
    }
}