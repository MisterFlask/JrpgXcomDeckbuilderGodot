using Godot;
using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Intents
{
    public class StunnedIntent : AbstractIntent
    {
        public StunnedIntent(AbstractBattleUnit source) : base(source, new List<AbstractBattleUnit>(), null) //todo
        {
            ProtoSprite = IntentIcons.DazedIntent;
        }

        public override string GetGenericDescription()
        {
            return "This unit is stunned for the turn.";
        }

        public override string GetOverlayText()
        {
            return "";
        }

        protected override void Execute()
        {

        }

        protected override IntentPrefab GeneratePrefab(Node2D parent)
        {
            var parentPrefab = ServiceLocator.GameObjectTemplates().AttackPrefab;
            var returnedPrefab = parentPrefab.Spawn(parent.transform);
            return returnedPrefab;
        }
    }
}