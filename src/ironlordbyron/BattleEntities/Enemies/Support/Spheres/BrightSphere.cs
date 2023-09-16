using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections.Generic;

namespace Assets.CodeAssets.BattleEntities.Enemies.Summer
{
    public class BrighteningAura : AbstractStatusEffect
    {
        public BrighteningAura()
        {
            this.Name = "Brightening Aura";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("path_to_brightening_aura_icon");
            this.Stacks = 2;
        }

        public override string Description => $"When a non-Sphere is targeted by a card, grant all non-Sphere enemies {Stacks} strength.";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool cardIsOwnedByMe)
        {
            if (!targetOfCard.CharacterNicknameOrEnemyName.Contains("Sphere"))
            {
                foreach (var enemy in GameState.Instance.EnemyUnitsInBattle)
                {
                    if (!enemy.CharacterNicknameOrEnemyName.Contains("Sphere"))
                    {
                        var strengthEffect = enemy.GetStatusEffect<StrengthStatusEffect>();
                        if (strengthEffect != null)
                        {
                            strengthEffect.Stacks += Stacks;
                        }
                        else
                        {
                            enemy.StatusEffects.Add(new StrengthStatusEffect { Stacks = Stacks });
                        }
                    }
                }
            }
        }
    }

    public class BrightSphere : AbstractBattleUnit
    {
        public BrightSphere()
        {
            CharacterNicknameOrEnemyName = "Bright Sphere";
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/PathToBrightSphereSprite");
            MaxHp = 20;

            // Adding the status effect for Bright Sphere
            this.StatusEffects.Add(new BrighteningAura()
            {
                Stacks = 2
            });
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            // Always intends to shield itself
            return IntentsFromBaseDamage.DefendSelf(this, 5);
        }
    }
}
