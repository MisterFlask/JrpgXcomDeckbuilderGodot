using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Common
{
    public class InfiniteWorldsAllGrim : AbstractCard
    {
        // Cost: 2
        // apply 8 defense.  Apply 5 Terror to ALL enemies.  Draw two cards.

        public InfiniteWorldsAllGrim()
        {
            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            Name = "Infinite worlds, all grim";
            BaseDefenseValue = 8;
            TargetType = TargetType.ALLY;
            CardType = CardType.SkillCard;
            StaticBaseEnergyCost = 2;
            ProtoSprite = ProtoGameSprite.DiabolistIcon("reaper-scythe");
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} defense.  Apply 5 Binding to ALL enemies.  Draw two cards.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyDefense(target, Owner, BaseDefenseValue);
            foreach (var enemy in state().EnemyUnitsInBattle)
            {
                action().ApplyStatusEffect(enemy, new BindingStatusEffect());
            }
            action().DrawCards(2);
        }
    }
}