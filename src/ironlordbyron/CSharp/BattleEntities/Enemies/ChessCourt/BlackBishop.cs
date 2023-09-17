using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.ChessCourt
{
    public class BlackBishop : AbstractEnemyUnit
    {
        public BlackBishop()
        {
            //difficulty 3
            CharacterNicknameOrEnemyName = "Red Bishop";
            ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites\\Enemies\\v2\\Corrupted Legendary Knight Arriette");
        }

        public override void AssignStatusEffectsOnCombatStart()
        {
            StatusEffects.Add(new BlackBishopStatusEffect()
            {
                Stacks = 2
            });
        }

        //Black Bishop/Red Bishop: All-Around Helper attack pattern for each.  
        //Black bishop:  When it deals at least 8 combat damage, gain 2 strength.  
        //Red: When it does at least 8 combat damage, ALL soldiers gains 15 stress.
        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.RandomIntent(
                IntentsFromBaseDamage.AttackRandomPc(this, 8, 2),
                IntentsFromBaseDamage.AttackRandomPc(this, 20, 1),
                IntentsFromBaseDamage.DefendSelf(this, 5));
        }
    }

    public class BlackBishopStatusEffect : AbstractStatusEffect
    {
        // when deals at least 8 combat damage, gain 2 strength
        public override string Description => "Whenever this unit deals at least 8 combat damage, gain [stacks] strength.";

        public override void OnStriking(AbstractBattleUnit unitStruck, AbstractCard cardUsedIfAny, int damageAfterBlockingAndModifiers)
        {
            if (damageAfterBlockingAndModifiers > 0)
            {
                ActionManager.Instance.ApplyStatusEffect(OwnerUnit, new StrengthStatusEffect(), 2);
            }
        }
    }
}