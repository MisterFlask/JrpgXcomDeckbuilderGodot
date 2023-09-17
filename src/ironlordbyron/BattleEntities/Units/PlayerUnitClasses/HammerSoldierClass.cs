using System.Collections;

namespace Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses
{
    public class HammerSoldierClass : AbstractSoldierClass
    {
        public HammerSoldierClass()
        {
            EmblemIcon = ProtoGameSprite.EmblemIcon("hammer-break");
            PortraitFolder = "HammerPortraits";
        }
        public override string Name()
        {
            return "Hammer";
        }
    }
}