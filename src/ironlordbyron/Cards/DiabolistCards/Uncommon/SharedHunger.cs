using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Uncommon
{
    public class SharedHunger : AbstractCard
    {

        public SharedHunger()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            SetCommonCardAttributes("Shared Hunger", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1);

            ProtoSprite = ProtoGameSprite.DiabolistIcon("gym-pendant");
        }

        // Power:
        // After you play a card with "blood" in the name, Exhaust it and ALL characters heal 2.  Leadership: Heal 4 instead.
        public override string DescriptionInner()
        {
            return $"After you play a card with 'blood' in the name, exhaust it and ALL allies heal 2";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(Owner, new SharedHungerStatusEffect(), 2);
        }
    }

    public class SharedHungerStatusEffect: AbstractStatusEffect
    {
        public SharedHungerStatusEffect()
        {
            Name = "Shared Hunger Power";
        }

        public override string Description => $"After you play a card with \"blood\" in the name, Exhaust it and ALL allies heal {Stacks}.";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool isMine)
        {
            if (cardPlayed.NameContains("blood"))
            {
                foreach(var ally in state().AllyUnitsInBattle)
                {
                    action().HealUnit(ally, Stacks, this.OwnerUnit);
                }
            }
        }
    }
}