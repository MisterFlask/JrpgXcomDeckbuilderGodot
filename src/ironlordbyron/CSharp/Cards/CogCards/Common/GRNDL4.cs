namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.CogCards.Common
{
    public class GRNDL4 : AbstractCard
    {
        public GRNDL4()
        {
            SetCommonCardAttributes("GRNDL XL", Rarity.COMMON, TargetType.ALLY, CardType.AttackCard, 1);
            BaseDamage = 4;
            ProtoSprite = ProtoGameSprite.CogIcon("shambling-mound");
        }

        // Apply 2 Charged to target ally.  Exhaust.
        // Technocannibalize:  Then, deal 4 damage and 4 Fumes to ALL enemies.
        public override string DescriptionInner()
        {
            return $"Apply 2 Charged to target ally.  Technocannibalize: Then, deal {DisplayedDamage()} damage and 4 Fumes to ALL enemies.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new ChargedStatusEffect(), 2);
            CardAbilityProcs.Technocannibalize(this, () =>
            {
                foreach (var enemy in state().EnemyUnitsInBattle)
                {
                    action().ApplyStatusEffect(enemy, new FumesStatusEffect(), 4);
                    action().AttackWithCard(this, enemy);
                }
            });
        }
    }
}