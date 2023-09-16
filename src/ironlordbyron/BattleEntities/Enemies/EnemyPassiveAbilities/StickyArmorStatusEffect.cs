using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class StickyArmorStatusEffect : AbstractStatusEffect
    {
        public StickyArmorStatusEffect()
        {
            Name = "Sticky Armor";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("sticky-boot");
        }

        /// whenever this character is targeted by a card, the card's cost increases by 1 for the combat.
        public override string Description => "Whenever this character is targeted by a card, the card's cost increases by 1 for the combat.";


        public override void OnTargetedByCard(AbstractCard sourceCard)
        {
            sourceCard.PersistentCostModifiers.Add(new RestOfCombatCostModifier(1));
        }
    }
}