namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class EruditionStatusEffect : AbstractStatusEffect
    {
        public EruditionStatusEffect()
        {
            Name = "Erudition";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("smart");

        }

        public override string Description => "Each time you play 3 cards, this character gains [stacks] block.";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool cardIsOwnedByMe)
        {
            SecondaryStacks++;

            if (SecondaryStacks >= 3)
            {
                SecondaryStacks = 0;
                ActionManager.Instance.ApplyDefense(OwnerUnit, null, Stacks);
            }
        }
    }
}