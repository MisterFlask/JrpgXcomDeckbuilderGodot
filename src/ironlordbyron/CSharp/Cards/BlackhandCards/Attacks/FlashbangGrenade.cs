namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.BlackhandCards.Attacks
{
    public class FlashbangGrenade : AbstractCard
    {
        // Apply 1 Vulnerable to all enemies.  Ambush: Gain 1 energy.  Exhaust.  Cost 0.

        public FlashbangGrenade()
        {
            SetCommonCardAttributes("Flashbang", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);
            BaseDamage = 1;
            Stickers.Add(new BasicAttackTargetSticker());
            ProtoSprite = ProtoGameSprite.BlackhandIcon("flash-grenade");

        }

        public override string DescriptionInner()
        {
            return $"Apply 1 Vulnerable to all enemies.  Ambush:  Gain 1 energy.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach (var enemy in state().EnemyUnitsInBattle)
            {
                action().ApplyStatusEffect(enemy, new VulnerableStatusEffect(), 1);
            }
            this.Ambush(() =>
            {
                state().energy++;
            });
        }
    }
}