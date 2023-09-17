using System.Collections;
using Assets.CodeAssets.Cards;

public class Parasite : AbstractCard
{
    public Parasite()
    {
        this.Name = "Parasite";
    }

    public override string DescriptionInner()
    {
        return $"Exhaust.  If not played: Take 5 damage at end of turn.";
    }

    public override void InHandAtEndOfTurnAction()
    {
        action().DamageUnitNonAttack(Owner, null, 5);
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        ActionManager.Instance.ExhaustCard(this);
    }
}
