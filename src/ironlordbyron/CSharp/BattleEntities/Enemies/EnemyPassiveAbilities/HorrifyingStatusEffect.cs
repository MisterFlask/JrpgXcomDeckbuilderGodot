namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class HorrifyingStatusEffect : AbstractStatusEffect
    {
        public HorrifyingStatusEffect()
        {
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("eyeball");

            Name = "Horrifying";
        }

        public override string Description => "Whenever this character is the target of a card, the targeting character gains [stacks] stress.";

        public override void OnTargetedByCard(AbstractCard sourceCard)
        {
            ActionManager.Instance.ApplyStress(OwnerUnit, stressApplied: Stacks);
        }
    }
}