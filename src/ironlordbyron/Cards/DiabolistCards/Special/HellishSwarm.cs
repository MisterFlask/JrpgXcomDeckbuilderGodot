using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.Stickers;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Special
{
    public class HellishSwarm : AbstractCard
    {
        public HellishSwarm()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Hellish Swarm", Rarity.NOT_IN_POOL, TargetType.ENEMY, CardType.AttackCard, 1);
            this.Stickers.Add(new HazardousCardSticker());
            this.BaseDamage = 2;
            ProtoSprite = ProtoGameSprite.DiabolistIcon("spider-eye");
        }

        // Deal 4 damage and apply 1 Vulnerable.  Draw a card.  Exhaust.  Cost 1.
        // Swarm.
        public override string DescriptionInner()
        {
            return "Deal 2 damage and apply 1 Vulnerable.  Light.  Nascent.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new VulnerableStatusEffect(), 1);
            this.Action_Exhaust();
        }
    }
}