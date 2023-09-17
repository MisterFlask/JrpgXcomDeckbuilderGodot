using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;

namespace Assets.CodeAssets.Cards.HammerCards.Common
{
    public class WhatDidYouSayAboutMe : AbstractCard
    {
        // Gain 5 Retaliate.  Taunt ALL enemies.
        // Brute: Draw a card.

        public WhatDidYouSayAboutMe()
        {
            SetCommonCardAttributes("What Did You Just Say About Me", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1, typeof(HammerSoldierClass));

            ProtoSprite = ProtoGameSprite.HammerIcon("flying-fox");
        }

        public override string DescriptionInner()
        {
            return "Gain 5 Retaliate.  Taunt ALL enemies.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToOwner(new RetaliateStatusEffect(), 5);
            foreach(var enemy in state().EnemyUnitsInBattle)
            {
                action().TauntEnemy(enemy, Owner);
            }
        }
    }
}