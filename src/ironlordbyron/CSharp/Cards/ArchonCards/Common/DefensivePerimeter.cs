using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Common
{
    public class DefensivePerimeter : AbstractCard
    {
        public DefensivePerimeter()
        {
            SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            SetCommonCardAttributes("Defensive Perimeter", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 1,
                protoGameSprite: ProtoGameSprite.ArchonIcon("gate"));
        }

        /// apply 8 defense.  Leadership:  Each ally gains 2 Temporary Dexterity.
        public override string DescriptionInner()
        {
            return $"Apply 8 defense.  Leadership:  Each ally gains 8 Temporary HP.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyDefense(target, Owner, BaseDefenseValue);
            this.PerformLeadershipAction(() =>
            {
                foreach (var ally in state().AllyUnitsInBattle)
                {
                    action().ApplyStatusEffect(ally, new TemporaryHpStatusEffect(), 8);
                }
            });
        }
    }


}