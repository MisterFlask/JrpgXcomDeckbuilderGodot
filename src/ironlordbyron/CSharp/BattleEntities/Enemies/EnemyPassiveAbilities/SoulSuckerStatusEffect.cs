namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities
{
    // Whenever ANY enemy in the combat dies, gains +3 strength.
    public class SoulSuckerStatusEffect : AbstractStatusEffect
    {
        public SoulSuckerStatusEffect()
        {
            Name = "Soul Sucker";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("lamprey-mouth");
        }

        public override string Description => "Whenever a unit dies in combat, gain [stacks] strength.";

        public override void ProcessProc(AbstractProc proc)
        {
            if (proc is CharacterDeathProc)
            {
                ActionManager.Instance.ApplyStatusEffect(OwnerUnit, new StrengthStatusEffect(), Stacks);
            }
        }
    }
}