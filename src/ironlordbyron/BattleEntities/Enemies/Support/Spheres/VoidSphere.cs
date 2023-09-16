using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections.Generic;

namespace Assets.CodeAssets.BattleEntities.Enemies.Summer
{
    public class VoidPresence : AbstractStatusEffect
    {
        public VoidPresence()
        {
            this.Name = "Void Presence";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("path_to_void_presence_icon");
            this.Stacks = 1; // Assuming that each instance of this effect applies 1 stress. Modify if needed.
        }

        public override string Description => $"When a non-Sphere is targeted by a card, inflict Stress on the owner of that card.";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool cardIsOwnedByMe)
        {
            if (!targetOfCard.CharacterNicknameOrEnemyName.Contains("Sphere"))
            {
                // Assuming Player is a representation of the player character and has a method to apply status effects.
                ActionManager.Instance.ApplyStatusEffect(cardPlayed.Owner,  new StressStatusEffect { Stacks = Stacks });
            }
        }
    }

    public class VoidSphere : AbstractBattleUnit
    {
        public VoidSphere()
        {
            CharacterNicknameOrEnemyName = "Void Sphere";
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/PathToVoidSphereSprite");
            MaxHp = 20;

            // Adding the status effect for Void Sphere
            this.StatusEffects.Add(new VoidPresence
            {
                Stacks = 3 // Set the default stacks for Void Presence.
            });
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            // Always intends to shield itself
            return IntentsFromBaseDamage.DefendSelf(this, 5);
        }
    }
}
