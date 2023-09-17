using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;

namespace Assets.CodeAssets.Cards.BlackhandCards.Skills
{
    public class FireShelter : AbstractCard
    {
        // Apply 4 Temporary Thorns and 20 defense to the target.
        // Cost 2.

        public FireShelter()
        {
            SetCommonCardAttributes("Fire Shelter", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 2, typeof(BlackhandSoldierClass));
            ProtoSprite = ProtoGameSprite.BlackhandIcon("dog-house");
        }

        public override string DescriptionInner()
        {
            return $"Apply 4 Temporary Thorns and {DisplayedDefense()} defense to ALL allies.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach(var ally in state().AllyUnitsInBattle)
            {
                action().ApplyStatusEffect(ally, new TemporaryThorns(), 4);
                action().ApplyDefense(ally, this.Owner, BaseDefenseValue);
            }
        }
    }
}