using System.Collections;

namespace Assets.CodeAssets.BattleEntities.Intents
{
    using System.Collections;
    using System.Collections.Generic;

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
        protected override IntentPrefab GeneratePrefab(GameObject parent)
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