using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects;
using GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic.BattleRules;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Common
{
    public class GazeFromTheAbyss : AbstractCard
    {
        // Apply 20 Terror and 1 Vulnerable to an enemy.
        // cost 2
        public GazeFromTheAbyss()
        {
            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            SetCommonCardAttributes("Abyssal Gaze", Rarity.COMMON, TargetType.ENEMY, CardType.SkillCard, 2);
            BaseDefenseValue = 12;
            ProtoSprite = ProtoGameSprite.DiabolistIcon("terror");
            MagicNumber = 20;
        }

        public override EnergyPaidInformation GetNetEnergyCost()
        {
            return BloodpriceBattleRules.GetNetEnergyCostWithBloodprice(this);
        }

        public override string DescriptionInner()
        {
            return $"Apply {MagicNumber} Binding and 1 Vulnerable to the enemy.  Bloodprice.";
        }


        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new BindingStatusEffect(), MagicNumber);
            action().ApplyStatusEffect(target, new VulnerableStatusEffect(), 1);
        }

    }
}