using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections.Generic;

namespace Assets.CodeAssets.BattleEntities.Enemies.Summer
{
    public class ProtectiveFlames : AbstractStatusEffect
    {
        public ProtectiveFlames()
        {
            this.Name = "Protective Flames";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("path_to_protective_flames_icon");
            this.Stacks = 5;
        }

        public override string Description => $"If any non-Sphere is targeted by a card, Flaming Sphere deals {Stacks} damage to them.";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool cardIsOwnedByMe)
        {
                if (!targetOfCard.CharacterNicknameOrEnemyName.Contains("Sphere"))
                {
                    ActionManager.Instance.DamageUnitNonAttack(cardPlayed.Owner, this.OwnerUnit, Stacks);
                }
            }
        }

    public class FlamingSphere : AbstractBattleUnit
    {
        public FlamingSphere()
        {
            CharacterNicknameOrEnemyName = "Flaming Sphere";
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/PathToFlamingSphereSprite");
            MaxHp = 20;

            // Adding the status effect for Flaming Sphere
            this.StatusEffects.Add(new ProtectiveFlames { Stacks = 5});
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            // Always intends to shield itself
            return IntentsFromBaseDamage.DefendSelf(this, 5);
        }
    }
}