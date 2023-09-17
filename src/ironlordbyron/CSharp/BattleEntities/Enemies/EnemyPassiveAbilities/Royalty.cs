namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class Royalty : AbstractStatusEffect
    {
        public Royalty()
        {
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("royal-love");
            Name = "Royal";
        }
        public override string Description => "This character takes half damage from Common cards.";

        public override void ModifyPostBlockDamageTaken(DamageBlob damageBlob)
        {
            if (damageBlob.CardIfAny?.Rarity == Rarity.COMMON)
            {
                damageBlob.Damage = damageBlob.Damage / 2;
            }
        }
    }
}