namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class AppalledStatusEffect : AbstractStatusEffect
    {
        public AppalledStatusEffect()
        {
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("surprised-skull");
            Name = "Appalled";
        }

        // lose [Stacks] strength each time attacked, to a minimum of -3.  Penalty decreases each turn.
        public override string Description => $"Lose {DisplayedStacks()} strength each time attacked.  " +
            "Strength reset to 0 at start of turn.";


        public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
        {
            ActionManager.Instance.ApplyStatusEffect(OwnerUnit, new StrengthStatusEffect(), -1);
        }

        public override void OnTurnStart()
        {
            OwnerUnit.RemoveStatusEffect<StrengthStatusEffect>();
        }
    }
}