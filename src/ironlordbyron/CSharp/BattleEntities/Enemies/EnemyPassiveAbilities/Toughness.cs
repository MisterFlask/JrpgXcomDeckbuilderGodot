namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class Toughness : AbstractStatusEffect
    {
        public Toughness()
        {
            Name = "Toughness";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("buffalo-head");
        }

        public override string Description => "Reduces ALL sources of damage by 1 per stack.";

        public override int DamageReceivedAddition()
        {
            return -1 * Stacks;
        }
    }
}