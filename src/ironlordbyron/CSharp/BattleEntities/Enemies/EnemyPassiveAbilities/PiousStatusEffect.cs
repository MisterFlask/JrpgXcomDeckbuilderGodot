namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class PiousStatusEffect : AbstractStatusEffect
    {
        public PiousStatusEffect()
        {
            Name = "Pious";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("prayer");
        }

        // it costs 1 more energy to target this character with a card.
        public override string Description => "It costs 1 more energy to target this character with a card.";

        public override int GetTargetedCostModifier(AbstractCard card)
        {
            return 1;
        }
    }
}