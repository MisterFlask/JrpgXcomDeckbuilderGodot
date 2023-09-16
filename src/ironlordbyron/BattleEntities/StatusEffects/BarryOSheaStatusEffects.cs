using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections.Generic;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class NoRush : AbstractStatusEffect
    {
        public NoRush()
        {
            Name = "No Rush";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("sea-turtle");
        }

        public override string Description => $"Whenever you play a card, all allies gain -{DisplayedStacks()} temp str.";

        // This method is triggered whenever any card is played.
        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool cardIsOwnedByMe)
        {
            foreach (var ally in GameState.Instance.AllyUnitsInBattle)
            {
                var tempStrengthEffect = ally.GetStatusEffect<TemporaryStrengthStatusEffect>();
                if (tempStrengthEffect != null)
                {
                    tempStrengthEffect.Stacks -= Stacks;
                }
                else
                {
                    ally.StatusEffects.Add(new TemporaryStrengthStatusEffect { Stacks = -Stacks });
                }
            }
        }
    }

    public class Growing : AbstractStatusEffect
    {
        public Growing()
        {
            Name = "Growing";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("pine-tree");
        }

        public override string Description => $"Gain +{DisplayedStacks()} strength per turn.";

        // This method is triggered at the start of the unit's turn.
        public override void OnTurnStart()
        {
            var strengthEffect = this.OwnerUnit.GetStatusEffect<StrengthStatusEffect>();
            if (strengthEffect != null)
            {
                strengthEffect.Stacks += Stacks;
            }
            else
            {
                this.OwnerUnit.StatusEffects.Add(new StrengthStatusEffect { Stacks = Stacks });
            }
        }
    }
}
