namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses
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