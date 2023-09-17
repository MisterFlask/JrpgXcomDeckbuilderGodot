using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Rare
{
    public class BackToThePit : AbstractCard
    {

        public BackToThePit()
        {
            SetCommonCardAttributes("Back To The Pit", Rarity.RARE, TargetType.ENEMY, CardType.AttackCard, 3, typeof(DiabolistSoldierClass));

            DamageModifiers.Add(new BackToThePitLethalDamageRule());

            BaseDamage = 30;

            DamageModifiers.Add(new SlayerDamageModifier());
        }

        public override string DescriptionInner()
        {
            return $"Deal {BaseDamage} damage. Draw 3 cards.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(Owner, new StressStatusEffect(), -3);
            Action_Exhaust();
        }
    }

    public class BackToThePitLethalDamageRule : DamageModifier
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