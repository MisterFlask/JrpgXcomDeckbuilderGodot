using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses
{
    public class DiabolistSoldierClass : AbstractSoldierClass
    {
        public DiabolistSoldierClass()
        {
            EmblemIcon = ProtoGameSprite.EmblemIcon("horned-skull");
            PortraitFolder = "DiabolistPortraits";
        }
        public override string Name()
        {

            return "Diabolist";
        }

        public override List<AbstractCard> StartingCards()
        {
            return new List<AbstractCard>()
            {
                new DiabolistAttack(),
                new DiabolistAttack(),
                new DiabolistDefend(),
                new DiabolistDefend()

            };
        }
    }
}