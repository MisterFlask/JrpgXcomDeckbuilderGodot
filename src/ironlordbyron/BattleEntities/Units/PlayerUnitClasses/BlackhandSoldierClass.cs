using Assets.CodeAssets.Cards.BlackhandCards.Attacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses
{
    public class BlackhandSoldierClass : AbstractSoldierClass
    {
        public BlackhandSoldierClass()
        {
            EmblemIcon = ProtoGameSprite.EmblemIcon("flamethrower-soldier");
            PortraitFolder = "BlackhandPortraits";
        }

        public override string Name()
        {
            return "Blackhand";
        }

        public override List<AbstractCard> StartingCards()
        {
            return new List<AbstractCard>()
            {
                new Flamer(),
                new SlashAndBurn()
            };
        }
    }
}