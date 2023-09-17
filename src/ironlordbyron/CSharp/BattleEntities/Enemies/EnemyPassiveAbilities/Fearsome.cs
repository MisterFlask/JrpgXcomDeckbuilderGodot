namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class Fearsome : AbstractStatusEffect
    {
        public Fearsome()
        {
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("gooey-eyed-sun");
            Name = "Fearsome";
        }

        public override string Description => $"Whenever this deals unblocked damage, apply {DisplayedStacks()} stress to the character.";

        public override void OnStriking(AbstractBattleUnit unitStruck, AbstractCard cardUsedIfAny, int damageAfterBlockingAndModifiers)
        {
            if (damageAfterBlockingAndModifiers > 0)
            {
                ActionManager.Instance.ApplyStress(unitStruck, stressApplied: Stacks);
            }
        }
    }
}