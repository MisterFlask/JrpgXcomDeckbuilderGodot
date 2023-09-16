using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Starting
{
    public class ArchonDefend : Defend
    {
        public ArchonDefend()
        {
            ProtoSprite = ProtoGameSprite.ArchonIcon("shield-reflect");
        }
    }
}