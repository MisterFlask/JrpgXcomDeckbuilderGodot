using Assets.CodeAssets.BattleEntities.StatusEffects;
using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;

namespace Assets.CodeAssets.Cards.HammerCards.Uncommon
{
    public class HammerSpecialVintage : AbstractCard
    {

        public HammerSpecialVintage()
        {
            this.SoldierClassCardPools.Add(typeof(HammerSoldierClass));

            SetCommonCardAttributes("Heavy Drinker", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, 
                CardType.SkillCard, 1, typeof(HammerSoldierClass));
            ProtoSprite = ProtoGameSprite.HammerIcon("beer-bottle");

        }

        public override string DescriptionInner()
        {
            return $"Apply 3 Strength, Barricade 4 and 3 Groggy.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToTarget(new StrengthStatusEffect(), 3, target);
            Action_ApplyStatusEffectToTarget(new BarricadeStatusEffect(), 4, target);
            Action_ApplyStatusEffectToTarget(new GroggyStatusEffect(), 3, target);
        }
    }
}