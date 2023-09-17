using Godot;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Intents
{
    public class ChargingIntent : AbstractIntent
    {
        public ChargingIntent(AbstractBattleUnit source) : base(source,
            source.ToSingletonList(),
            IntentIcons.UnknownIntent)
        {
        }

        public override string GetGenericDescription()
        {
            return "This unit is charging up!";
        }
        protected override IntentPrefab GeneratePrefab(Node2D parent)
        {
            // TODO:  Fix prefab
            var parentPrefab = ServiceLocator.GameObjectTemplates().AttackPrefab;
            return parentPrefab.Spawn(parent.transform);
        }

        protected override void Execute()
        {
            // nothing happens
        }

        public override string GetOverlayText()
        {
            return $"";
        }
    }
}