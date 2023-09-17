namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.BlackhandCards.Powers
{
    public class DeadMansSwitch : AbstractCard
    {
        // At the beginning of your turn, apply 2 Fumes to a random enemy.  Cost 0.
        // If this character dies, apply 40 burning to ALL enemies.

        public DeadMansSwitch()
        {
            SetCommonCardAttributes("Dead Man's Switch", Rarity.UNCOMMON, TargetType.ALLY, CardType.SkillCard, 1);
            ProtoSprite = ProtoGameSprite.BlackhandIcon("half-dead");
        }

        public override string DescriptionInner()
        {
            return "At the beginning of your turn, apply 2 Fumes to a random enemy.  " +
                "If this character dies, apply 20 burning to ALL enemies.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(Owner, new DeathCurseStatusEffect(), 20);
            action().ApplyStatusEffect(Owner, new FumeSpewerStatusEffect(), 2);
        }
    }
    public class FumeSpewerStatusEffect : AbstractStatusEffect
    {
        public FumeSpewerStatusEffect()
        {
            Name = "Fume Spewer";
        }

        public override void OnTurnEnd()
        {

            var randomEnemy = GameState.Instance.EnemyUnitsInBattle.PickRandomWhere(item => item.IsTargetable());
            ActionManager.Instance.ApplyStatusEffect(randomEnemy, new FumesStatusEffect(), Stacks);
        }

        public override string Description => $"At the beginning of your turn, apply {DisplayedStacks()} Fumes to a random enemy.";
    }

    public class DeathCurseStatusEffect : AbstractStatusEffect
    {
        public DeathCurseStatusEffect()
        {
            Name = "Death Curse";
        }


        public override void OnDeath(AbstractBattleUnit unitThatKilledMe, AbstractCard cardPlayedIfAny)
        {
            foreach (var enemy in GameState.Instance.EnemyUnitsInBattle)
            {
                ActionManager.Instance.ApplyStatusEffect(enemy, new BurningStatusEffect(), Stacks);
            }
        }

        public override string Description => $"When this character dies, apply {DisplayedStacks()} burning to ALL enemies";
    }

}