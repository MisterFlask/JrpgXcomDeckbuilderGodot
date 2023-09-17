using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.HammerCards.Rare
{
    public class Bastion : AbstractCard
    {
        // apply 10 Barricade to ALL allies. [10 damage resist, halves each turn.  Barricade scales with Dexterity.]  Cost 3, Refund 1.  Exhaust.

        public Bastion()
        {
            SoldierClassCardPools.Add(typeof(HammerSoldierClass));

            SetCommonCardAttributes("Bastion", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 3);
            ProtoSprite = ProtoGameSprite.HammerIcon("fancy-castle");

        }

        public override string DescriptionInner()
        {
            return $"apply 10 Barricade to ALL allies.  Refund 1.  Exhaust";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach (var ally in state().AllyUnitsInBattle)
            {
                Action_ApplyStatusEffectToTarget(new BarricadeStatusEffect(), 10, ally);
            }
            CardAbilityProcs.Refund(this);
            Action_Exhaust();
        }
    }
}