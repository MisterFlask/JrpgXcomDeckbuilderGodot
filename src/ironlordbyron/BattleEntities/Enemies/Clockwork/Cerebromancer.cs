using Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities;
using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections.Generic;
using static UnityEngine.UI.GridLayoutGroup;

namespace Assets.CodeAssets.BattleEntities.Enemies.Summer
{

    public class Metallicize : AbstractStatusEffect
    {
        public Metallicize()
        {
            this.Name = "Metallicize";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("metal_scales");
        }

        public override string Description => $"Grants {DisplayedStacks()} block at the start of the turn.";

        public override void OnTurnStart()
        {
            ActionManager.Instance.ApplyDefense(OwnerUnit, null, Stacks);
        }
    }

    public class Cerebromancer : AbstractBattleUnit
    {
        public Cerebromancer()
        {
            CharacterNicknameOrEnemyName = "Cerebromancer";
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites\\Enemies\\v2\\Boss Khronos"); //path: "Sprites/Enemies/PathToBarrysSprite"
            MaxHp = 100; // Please specify the HP value here

            // Adding the initial status effects for Cerebromancer
            this.StatusEffects.Add(new Metallicize { Stacks = 10 });
            this.StatusEffects.Add(new Fearsome { Stacks = 5 });
            this.StatusEffects.Add(new Clockwork { Stacks = 5 });
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                new List<AbstractIntent> { new BuffSelfIntent(this, new StrengthStatusEffect { Stacks = 3 }) },
                IntentsFromBaseDamage.AttackSetOfPcs(this, 4, 2, 2),
                new List<AbstractIntent> { DebuffOtherIntent.StatusEffectToAllPcs(this, new WeakenedStatusEffect(), 2, new VulnerableStatusEffect(), 2) }
            );
        }
    }
}
