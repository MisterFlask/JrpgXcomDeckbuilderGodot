using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Skills
{
    public class SlowBoil : AbstractCard
    {
        // Shuffle three Smog Grenades into your draw pile.  Apply 8 defense to ALL allies.
        public SlowBoil()
        {
            SetCommonCardAttributes("Slow Boil", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 2, typeof(BlackhandSoldierClass));
            BaseDefenseValue = 8;
        }

        public override string DescriptionInner()
        {
            return $"Shuffle three Smog Grenades into your draw pile.  Apply 8 defense to ALL allies";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            for(int i = 0; i < 3; i++)
            {
                action().CreateCardToBattleDeckDrawPile(new SmogGrenade(), CardCreationLocation.SHUFFLE);
            }
            foreach(var ally in state().AllyUnitsInBattle)
            {
                action().ApplyDefense(ally, this.Owner, BaseDefenseValue);
            }
        }
    }
}