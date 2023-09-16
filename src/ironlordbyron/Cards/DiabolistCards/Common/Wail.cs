using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Common
{
    // cost 0.  Lose 3 stress.  Exhaust.
    public class Wail : AbstractCard
    {
        public Wail()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Wail", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);

            ProtoSprite = ProtoGameSprite.DiabolistIcon("terror");
        }

        public override string DescriptionInner()
        {
            return $"Relieve 3 stress from {ownerDisplayString()}.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(this.Owner, new StressStatusEffect(), -3);
            this.Action_Exhaust();
        }
    }
}