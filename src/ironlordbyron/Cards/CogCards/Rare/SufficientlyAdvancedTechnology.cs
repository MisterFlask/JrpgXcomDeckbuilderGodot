using System.Collections;

namespace Assets.CodeAssets.Cards.CogCards.Rare
{
    public class SufficientlyAdvancedTechnology : AbstractCard
    {
        // Whenever a card is created this combat, give it "Deal 10 damage to ALL enemies."  Gain 1 data point.

        public SufficientlyAdvancedTechnology()
        {
            SetCommonCardAttributes("Sufficiently Advanced Technology", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1);
            ProtoSprite = ProtoGameSprite.CogIcon("processor");
        }

        public override string DescriptionInner()
        {
            return $"Whenever a card is created this combat, give it 'Deal 10 damage to ALL enemies and take 5 stresss.'  Gain 1 data point.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToOwner(new SufficientlyAdvancedTechnologyStatusEffect(), 10);
            CardAbilityProcs.GainDataPoints(this, 1);
        }
    }


    public class SufficientlyAdvancedTechnologyStatusEffect : AbstractStatusEffect
    {
        public SufficientlyAdvancedTechnologyStatusEffect()
        {
            Name = "Sufficiently Advanced Technology";
        }
        public override string Description => $"Whenever a card is created this combat, give it 'Then, deal {DisplayedStacks()} damage to ALL enemies'.";

        public override void ProcessProc(AbstractProc proc)
        {
            if (proc is CardCreatedProc)
            {
                proc.TriggeringCardIfAny.AddSticker(new SufficientlyAdvancedTechnologyCardSticker
                {
                    Stacks = Stacks
                }) ;
            }
        }
    }


    public class SufficientlyAdvancedTechnologyCardSticker: AbstractCardSticker
    {
        public int Stacks { get; set; } = 10;

        public override string CardDescriptionAddendum()
        {
            return $"Then, deal {card.DisplayedDamage(Stacks)} damage to ALL enemies.";
        }

        public override void OnThisCardPlayed(AbstractCard card, AbstractBattleUnit target)
        {
            foreach(var enemy in GameState.Instance.EnemyUnitsInBattle)
            {
                ActionManager.Instance.AttackUnitForDamage(target, this.card.Owner, Stacks, card);
            }
        }
    }
}