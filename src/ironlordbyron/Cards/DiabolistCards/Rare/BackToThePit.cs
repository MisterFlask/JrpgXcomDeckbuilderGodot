using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;

namespace Assets.CodeAssets.Cards.DiabolistCards.Rare
{
    public class BackToThePit : AbstractCard
    {

        public BackToThePit()
        {
            this.SetCommonCardAttributes("Back To The Pit", Rarity.RARE, TargetType.ENEMY, CardType.AttackCard, 3, typeof(DiabolistSoldierClass));

            this.DamageModifiers.Add(new BackToThePitLethalDamageRule());

            BaseDamage = 30;

            this.DamageModifiers.Add(new SlayerDamageModifier());
        }

        public override string DescriptionInner()
        {
            return $"Deal {BaseDamage} damage. Draw 3 cards.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(this.Owner, new StressStatusEffect(), -3);
            this.Action_Exhaust();
        }
    }

    public class BackToThePitLethalDamageRule: DamageModifier
    {
        public BackToThePitLethalDamageRule()
        {
            TooltipDescription = "Lethal: Relieve 4 Stress for ALL allies.";
        }

        public override bool SlayInner(AbstractCard damageSource, AbstractBattleUnit target)
        {
            foreach (var ally in state().AllyUnitsInBattle)
            {
                action().ApplyStress(ally, -10);
            }
            return true;
        }
    }
    
}