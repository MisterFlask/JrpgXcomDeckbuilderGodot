using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Powers
{
    public class NapalmGrenade : AbstractCard
    {
        // apply 6 Burning to ALL enemies, then exhaust.  Cost 0.  Exhaust.

        public NapalmGrenade()
        {
            SetCommonCardAttributes("Napalm Grenade", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);
            Stickers.Add(new BasicAttackRandomEnemySticker());
            BaseDamage = 2;
            ProtoSprite = ProtoGameSprite.BlackhandIcon("grenade-a");
        }

        public override string DescriptionInner()
        {
            return $"Deal Apply 6 burning to ALL enemies.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach(var enemy in state().EnemyUnitsInBattle)
            {
                action().ApplyStatusEffect(enemy, new BurningStatusEffect(), 6);
            }
            this.Action_Exhaust();
        }
    }
}