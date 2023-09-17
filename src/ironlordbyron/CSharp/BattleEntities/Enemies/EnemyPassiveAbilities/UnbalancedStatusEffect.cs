﻿namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class UnbalancedStatusEffect : AbstractStatusEffect
    {
        public UnbalancedStatusEffect()
        {
            Name = "Unbalanced";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("unbalanced");
            SecondaryStacks = 0;
        }


        public override string Description => "Tbe first time this unit is attacked three times each turn, it is stunned for the turn.";

        public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
        {
            SecondaryStacks++;
            if (SecondaryStacks == 3)
            {
                ActionManager.Instance.ForceSwapIntents_RightNow(OwnerUnit, new StunnedIntent(OwnerUnit).ToSingletonList<AbstractIntent>());
            }
        }

        public override void OnTurnStart()
        {
            SecondaryStacks = 0;
        }
    }
}