using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Support.Spheres
{
    public class VoidPresence : AbstractStatusEffect
    {
        public VoidPresence()
        {
            Name = "Void Presence";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("path_to_void_presence_icon");
            Stacks = 1; // Assuming that each instance of this effect applies 1 stress. Modify if needed.
        }

        public override string Description => $"When a non-Sphere is targeted by a card, inflict Stress on the owner of that card.";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool cardIsOwnedByMe)
        {
            if (!targetOfCard.CharacterNicknameOrEnemyName.Contains("Sphere"))
            {
                // Assuming Player is a representation of the player character and has a method to apply status effects.
                ActionManager.Instance.ApplyStatusEffect(cardPlayed.Owner, new StressStatusEffect { Stacks = Stacks });
            }
        }
    }

    public class VoidSphere : AbstractBattleUnit
    {
        public VoidSphere()
        {
            CharacterNicknameOrEnemyName = "Void Sphere";
            ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/PathToVoidSphereSprite");
            MaxHp = 20;

            // Adding the status effect for Void Sphere
            StatusEffects.Add(new VoidPresence
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
