using System.Collections;
using Assets.CodeAssets.Cards;

public class Distracted : AbstractCard
{
    

    public Distracted()
    {
        this.Unplayable = true;
        this.Name = "Distracted";
    }

    public override string DescriptionInner()
    {
        return $"Unplayable.  Exhaust at the end of your turn.";
    }

    public override void InHandAtEndOfTurnAction()
    {
        ActionManager.Instance.ExhaustCard(this);
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
    }
}
