using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects;
using GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic.BattleRules;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Uncommon
{
    public class BloodOffering : AbstractCard
    {
        // Cost 7.  Apply 20 Temporary HP to all allies.  Bloodprice.

        public BloodOffering()
        {
            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));

            SetCommonCardAttributes("Blood Offering", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 7);
            Stickers.Add(new ExhaustCardSticker());
            ProtoSprite = ProtoGameSprite.DiabolistIcon("pentacle");
        }

        public override string DescriptionInner()
        {
            return "Apply 20 temporary HP to all allies.  Bloodprice.";
        }

        public override EnergyPaidInformation GetNetEnergyCost()
        {
            return BloodpriceBattleRules.GetNetEnergyCostWithBloodprice(this);
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach (var ally in state().AllyUnitsInBattle)
            {
                action().ApplyStatusEffect(ally, new TemporaryHpStatusEffect(), 20);
            }
            Action_Exhaust();
        }
    }
}