using Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities;
using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections.Generic;

namespace Assets.CodeAssets.BattleEntities.StatusEffects
{
    public class Clockwork : AbstractStatusEffect
    {
        public Clockwork()
        {
            this.Name = "Clockwork";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("clockwork"); // Placeholder: Assign an appropriate icon for this status effect.
            this.SecondaryStacks = 0;

        }

        public override string Description => $"Whenever the player plays [x] cards, this unit's strength is increased by 1.";
        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool cardIsOwnedByMe)
        {
            if (cardIsOwnedByMe) // Check if the card is played by the player.
            {
                this.SecondaryStacks += 1; // Decrement the counter.

                if (this.SecondaryStacks >= Stacks)
                {
                    // Reset the counter and apply strength.
                    this.SecondaryStacks = 0;
                    this.OwnerUnit.StatusEffects.Add(new StrengthStatusEffect { Stacks = 1 });
                }
            }
        }
    }
}

namespace Assets.CodeAssets.BattleEntities.Enemies.Summer
{
    public class ClockworkDrone : AbstractBattleUnit
    {
        public ClockworkDrone()
        {
            CharacterNicknameOrEnemyName = "Clockwork Drone";
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites\\Enemies\\v2\\Clockwork Mini A");
            MaxHp = 20;

            // Adding the initial status effect for Clockwork Drone.
            this.StatusEffects.Add(new Clockwork { Stacks = 5});
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.RandomIntent(
                IntentsFromBaseDamage.BuffSelf(this, new Growing { Stacks = 1 }),
                IntentsFromBaseDamage.AttackRandomPc(this, 4, 1)
            );
        }
    }
}
