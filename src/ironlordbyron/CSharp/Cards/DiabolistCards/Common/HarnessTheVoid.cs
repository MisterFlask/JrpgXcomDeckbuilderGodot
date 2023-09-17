using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Common
{
    public class HarnessTheVoid : AbstractCard
    {
        // Gain 10 Temp HP
        // Sacrifice: ALL allies gain 1 strength.

        public HarnessTheVoid()
        {
            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            SetCommonCardAttributes("Harness the Void", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 2);
            ProtoSprite = ProtoGameSprite.DiabolistIcon("pentagram-rose");
            MagicNumber = 10;
        }


        public override string DescriptionInner()
        {
            return $"Apply {MagicNumber} temp HP.  Sacrifice: exhaust, and ALL allies gain 1 strength.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new TemporaryHpStatusEffect(), MagicNumber);

            this.Sacrifice(() =>
            {
                Action_Exhaust();
                foreach (var ally in state().AllyUnitsInBattle)
                {
                    action().ApplyStatusEffect(target, new StrengthStatusEffect(), 1);
                }
            });
        }

    }
}