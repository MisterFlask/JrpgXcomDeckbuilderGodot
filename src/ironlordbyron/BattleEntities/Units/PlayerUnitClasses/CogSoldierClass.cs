using Assets.CodeAssets.Cards.CogCards.Starting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses
{
    public class CogSoldierClass : AbstractSoldierClass
    {

        public CogSoldierClass()
        {
            EmblemIcon = ProtoGameSprite.EmblemIcon("cog");
            PortraitFolder = "CogPortraits";
        }
        public override string Name()
        {
            return "Cog";
        }

        public override List<AbstractCard> StartingCards()
        {
            return new List<AbstractCard>
            {
                new CogAttack(),
                new CogAttack(),
                new CogDefend(),
                new CogDefend()
            };
        }

        public override string Description()
        {
            return "Archons tend to keep to themselves on base. This is because they need to maintain a calm distance from the employees whose lives will be in their hands, and not because nobody else wants to hang out with them off-hours.";
        }
    }
}