namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class BeautiousStatusEffect : AbstractStatusEffect
    {
        public BeautiousStatusEffect()
        {
            Name = "Beautious";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("gems");

        }

        public override string Description => "It costs [Stacks] extra energy to target this unit.";

        public override int GetTargetedCostModifier(AbstractCard card)
        {
            return Stacks;
        }
    }
}