using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities;
using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Clockwork
{

    public class Metallicize : AbstractStatusEffect
    {
        public Metallicize()
        {
            Name = "Metallicize";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("metal_scales");
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
            ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites\\Enemies\\v2\\Boss Khronos"); //path: "Sprites/Enemies/PathToBarrysSprite"
            MaxHp = 100; // Please specify the HP value here

            // Adding the initial status effects for Cerebromancer
            StatusEffects.Add(new Metallicize { Stacks = 10 });
            StatusEffects.Add(new Fearsome { Stacks = 5 });
            StatusEffects.Add(new Clockwork { Stacks = 5 });
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
