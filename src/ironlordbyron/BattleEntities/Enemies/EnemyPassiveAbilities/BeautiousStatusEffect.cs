using System.Collections;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class BeautiousStatusEffect : AbstractStatusEffect
    {
        public BeautiousStatusEffect()
        {
            Name = "Beautious";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("gems");

        }

        public override string Description => "It costs [Stacks] extra energy to target this unit.";

        public override int GetTargetedCostModifier(AbstractCard card)
        {
            return Stacks;
        }
    }
}