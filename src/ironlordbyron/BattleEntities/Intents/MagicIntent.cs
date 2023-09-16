using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Intents
{
    public class MagicIntent : AbstractIntent
    {
        public MagicIntent(AbstractBattleUnit source, Action toPerform) : base(source, null, IntentIcons.MagicIntent)
        {
            ToPerform = toPerform;
        }

        public Action ToPerform { get; }

        public override string GetGenericDescription()
        {
            return "This character is about to do something mysterious.";
        }

        public override string GetOverlayText()
        {
            return "";
        }

        protected override void Execute()
        {
            ToPerform();
        }

        protected override IntentPrefab GeneratePrefab(GameObject parent)
        {
            var parentPrefab = ServiceLocator.GameObjectTemplates().AttackPrefab;
            var returnedPrefab = parentPrefab.Spawn(parent.transform);
            return returnedPrefab;
        }

    }
}