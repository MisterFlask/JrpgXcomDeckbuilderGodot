using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.ChessCourt
{
    public class RedBishop : AbstractEnemyUnit
    {
        public RedBishop()
        {
            // difficulty 3
            CharacterNicknameOrEnemyName = "Red Bishop";
            MaxHp = 85;
            ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites\\Enemies\\v2\\Corrupted Legendary Knight Sen");
        }
        public override void AssignStatusEffectsOnCombatStart()
        {
            StatusEffects.Add(new Gloating()
            {
                Stacks = 30
            });
        }

        //Black Bishop/Red Bishop: All-Around Helper attack pattern for each.  
        //Black bishop:  When it deals at least 8 combat damage, gain 2 strength.  
        //Red: When it does at least 8 combat damage, ALL soldiers gains 15 stress.
        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.RandomIntent(
                IntentsFromBaseDamage.DefendSelf(this, 5),
                IntentsFromBaseDamage.AttackRandomPc(this, 8, 2));
        }
    }

    public class Gloating : AbstractStatusEffect
    {
        public Gloating()
        {
            Name = "Gloating";
            ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("imp-laugh");
        }

        // when deals at least 8 combat damage, gain 2 strength
        public override string Description => $"Whenever this unit deals at least 8 combat damage, it deals {DisplayedStacks()} stress to the unit it's damaging.";

        public override void OnStriking(AbstractBattleUnit unitStruck, AbstractCard cardUsedIfAny, int damageAfterBlockingAndModifiers)
        {
            if (damageAfterBlockingAndModifiers > 0)
            {
                ActionManager.Instance.ApplyStress(unitStruck, Stacks);
            }
        }
    }
}