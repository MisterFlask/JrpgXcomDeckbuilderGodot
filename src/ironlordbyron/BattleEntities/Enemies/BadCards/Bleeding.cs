using Assets.CodeAssets.Cards;
using System.Collections;

namespace Assets.CodeAssets.BattleEntities.Enemies.BadCards
{
    public class Bleeding : AbstractCard
    {
        private int Damage;

        public Bleeding()
        {
            Damage = 3;
            AddSticker(new ExhaustCardSticker());
        }

        public override string DescriptionInner()
        {
            return $"Retained: Take {Damage} damage";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
        }

        public override void InHandAtEndOfTurnAction()
        {
            ActionManager.Instance.DamageUnitNonAttack(Owner, null, Damage);
        }
    }
}