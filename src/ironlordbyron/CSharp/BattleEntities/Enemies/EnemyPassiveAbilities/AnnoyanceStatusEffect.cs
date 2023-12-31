﻿namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class AnnoyanceStatusEffect : AbstractStatusEffect
    {
        public AnnoyanceStatusEffect()
        {
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("angry-eyes");
            Name = "Annoyance";
        }
        public override string Description => $"This character gains {DisplayedStacks()} strength each time it's attacked.";

        public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
        {
            ActionManager.Instance.ApplyStatusEffect(OwnerUnit, new StrengthStatusEffect(), Stacks);

        }
    }
}