using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Clockwork;
using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.StatusEffects;
using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Clockwork
{
    public class Clockwork : AbstractStatusEffect
    {
        public Clockwork()
        {
            Name = "Clockwork";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("clockwork"); // Placeholder: Assign an appropriate icon for this status effect.
            SecondaryStacks = 0;

        }

        public override string Description => $"Whenever the player plays [x] cards, this unit's strength is increased by 1.";
        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool cardIsOwnedByMe)
        {
            if (cardIsOwnedByMe) // Check if the card is played by the player.
            {
                SecondaryStacks += 1; // Decrement the counter.

                if (SecondaryStacks >= Stacks)
                {
                    // Reset the counter and apply strength.
                    SecondaryStacks = 0;
                    OwnerUnit.StatusEffects.Add(new StrengthStatusEffect { Stacks = 1 });
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
            ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites\\Enemies\\v2\\Clockwork Mini A");
            MaxHp = 20;

            // Adding the initial status effect for Clockwork Drone.
            StatusEffects.Add(new Clockwork { Stacks = 5 });
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
