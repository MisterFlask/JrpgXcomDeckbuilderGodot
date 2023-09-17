using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Rare
{
    public class ForwardPlanning : AbstractCard
    {
        // Apply  Cost 1.

        public ForwardPlanning()
        {
            SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            SetCommonCardAttributes("Forward Planning", Rarity.RARE, TargetType.NO_TARGET_OR_SELF,
                CardType.SkillCard, 1,
                protoGameSprite: ProtoGameSprite.ArchonIcon("forward-sun"));
        }

        public override string DescriptionInner()
        {
            return "Apply 2 Weak and 2 Vulnerable to each enemy with Mark.  Add two Manuever to your hand. ";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach (var enemy in state().EnemyUnitsInBattle)
            {
                if (enemy.HasStatusEffect<MarkedStatusEffect>())
                {
                    action().ApplyStatusEffect(enemy, new VulnerableStatusEffect(), 2);
                    action().ApplyStatusEffect(enemy, new WeakenedStatusEffect(), 2);
                }
            }
            action().CreateCardToHand(new Manuever());
        }
    }
}