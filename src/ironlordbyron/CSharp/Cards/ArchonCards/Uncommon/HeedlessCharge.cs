namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Uncommon
{
    public class HeedlessCharge : AbstractCard
    {
        // deal 12 damage. ALL enemies gain 2 Vulnerable.  All allies gain +2 Temporary Strength.  Exert.  Cost 2.


        public HeedlessCharge()
        {
            SetCommonCardAttributes("Heedless Charge", Rarity.UNCOMMON, TargetType.ENEMY, CardType.AttackCard, 2,
                protoGameSprite: ProtoGameSprite.ArchonIcon("running-ninja"));
            BaseDamage = 12;
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage. ALL enemies gain 2 Vulnerable.  ALL allies gain 2 temporary strength. Exert.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            foreach (var ally in state().AllyUnitsInBattle)
            {
                action().ApplyStatusEffect(ally, new TemporaryStrengthStatusEffect(), 2);

            }

            foreach (var enemy in state().EnemyUnitsInBattle)
            {
                action().ApplyStatusEffect(enemy, new VulnerableStatusEffect(), 2);
            }

            this.ProcExert();
        }
    }
}