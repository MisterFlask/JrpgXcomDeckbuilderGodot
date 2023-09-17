namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class AboveItAll : AbstractStatusEffect
    {
        public AboveItAll()
        {
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("royal-love");
            Name = "AboveItAll";
        }
        public override string Description => "This character only takes damage from attacks.";

        public override void ModifyPostBlockDamageTaken(DamageBlob damageBlob)
        {
            if (!damageBlob.IsAttackDamage)
            {
                damageBlob.Damage = 0;
            }
        }
    }
}