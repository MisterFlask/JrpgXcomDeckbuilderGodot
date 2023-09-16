using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.DamageModifiers;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Rare
{
    public class F : AbstractCard
    {
        // Deal 10 damage.  Precision.  Bounty.  Cost 1.
        // TODO: If an ally dies while this is in your deck and not exhausted, gain a random Emblem.

        public F()
        {
            SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            SetCommonCardAttributes("F", Rarity.RARE, TargetType.ENEMY, CardType.AttackCard, 1,
                protoGameSprite: ProtoGameSprite.ArchonIcon("grave-flowers"));
            this.DamageModifiers.Add(BountyDamageModifier.Create());
            this.DamageModifiers.Add(new PrecisionDamageModifier());
            this.BaseDamage = 10;
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage.  Precision.  Bounty.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().AttackWithCard(this, target);
        }
    }
}