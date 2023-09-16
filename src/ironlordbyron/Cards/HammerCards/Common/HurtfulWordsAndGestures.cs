using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards.Common
{
    public class HurtfulWordsAndGestures : AbstractCard
    {
        // Taunt enemy.   Cost 0.
        public HurtfulWordsAndGestures()
        {
            SetCommonCardAttributes("Hurtful Words and Gestures", Rarity.NOT_IN_POOL, TargetType.ENEMY, CardType.SkillCard, 0);
            ProtoSprite = ProtoGameSprite.HammerIcon("profanity");

        }

        public override string DescriptionInner()
        {
            return "Taunt target enemy.  Exhaust.";
        }

        public override CanPlayCardQueryResult CanPlayInner(AbstractBattleUnit target)
        {
            if (BattleRules.IsEnemyEligibleForTaunting(target, this.Owner))
            {
                return CanPlayCardQueryResult.CanPlay();
            }
            else
            {
                return CanPlayCardQueryResult.CannotPlay("Taunted units must be attacking exactly one" +
                    " target who is not the taunting character.");
            }
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().TauntEnemy(target, this.Owner);
            Action_Exhaust();
        }
    }
}