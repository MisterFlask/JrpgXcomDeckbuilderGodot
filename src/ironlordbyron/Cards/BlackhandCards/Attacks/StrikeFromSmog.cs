using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Attacks
{
    public class StrikeFromSmog : AbstractCard
    {
        // Apply 10 Fumes to target enemy.  Then deal damage to it equal to its Fumes.

        public StrikeFromSmog()
        {
            SetCommonCardAttributes("Strike From Smog", Rarity.UNCOMMON, TargetType.ENEMY, CardType.SkillCard, 1);
            ProtoSprite = ProtoGameSprite.BlackhandIcon("cigar");

        }

        public override string DescriptionInner()
        {
            return "Apply 10 Fumes to target enemy.  Then deal damage to it equal to its Fumes.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new FumesStatusEffect(), 10);
            action().PushActionToBack("StrikeFromSmog_OnPlay", () =>
            {
                var damageToDo = target.GetStatusEffect<FumesStatusEffect>().Stacks + 10;
                action().DamageUnitNonAttack(target, this.Owner, damageToDo);
            });
        }
    }
}