namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities
{
    /// <summary>
    /// If this unit is struck three times in a single round,
    /// it flees the combat.  Right now we're just saying it dies.
    /// </summary>
    public class CowardiceStatusEffect : AbstractStatusEffect
    {
        public CowardiceStatusEffect()
        {
            Name = "Cowardice";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("run");
        }

        public override void OnRemoval()
        {
            ActionManager.Instance.KillUnit(OwnerUnit);
        }

        public override void ProcessProc(AbstractProc proc)
        {
            // dealt 20 or more damage to a foe
            // foe dies

            if (proc is CharacterDeathProc)
            {
                var death = (CharacterDeathProc)proc;
                if (death.CharacterDead.IsEnemy)
                {
                    Stacks--;
                }
            }

            if (proc is CharacterDamagedProc)
            {
                var damage = (CharacterDamagedProc)proc;
                if (proc.TriggeringCardIfAny != null
                    && damage.CharacterDamaged.IsEnemy
                    && damage.DamageInflicted > 19)
                {
                    Stacks--;
                }
            }
        }

        public override string Description => $"Whenever you deal >19 damage to any character with a card, or whenever another enemy dies, remove a stack.  Flees when all stacks are removed.";
    }
}