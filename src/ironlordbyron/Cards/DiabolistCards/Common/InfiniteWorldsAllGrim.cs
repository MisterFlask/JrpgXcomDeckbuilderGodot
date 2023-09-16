using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Common
{
    public class InfiniteWorldsAllGrim : AbstractCard
    {
        // Cost: 2
        // apply 8 defense.  Apply 5 Terror to ALL enemies.  Draw two cards.

        public InfiniteWorldsAllGrim()
        {
            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            Name = "Infinite worlds, all grim";
            BaseDefenseValue = 8;
            TargetType = TargetType.ALLY;
            CardType = CardType.SkillCard;
            this.StaticBaseEnergyCost = 2;
            ProtoSprite = ProtoGameSprite.DiabolistIcon("reaper-scythe");
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} defense.  Apply 5 Binding to ALL enemies.  Draw two cards.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyDefense(target, this.Owner, BaseDefenseValue);
            foreach(var enemy in state().EnemyUnitsInBattle)
            {
                action().ApplyStatusEffect(enemy, new BindingStatusEffect());
            }
            action().DrawCards(2);
        }
    }
}