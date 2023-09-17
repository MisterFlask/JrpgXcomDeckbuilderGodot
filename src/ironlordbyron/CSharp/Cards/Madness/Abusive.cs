using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards;
using System.Linq;

public class Abusive : AbstractCard
{
    public Abusive()
    {
        ProtoSprite = ProtoGameSprite.MadnessIcon("oni");
        Name = "Abusive";
    }

    public override string DescriptionInner()
    {
        return "At end of turn, deals 3 damage to your highest-health ally.";

    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {

    }

    public override void InHandAtEndOfTurnAction()
    {
        var allies = state().AllyUnitsInBattle.Where(item => item != this.Owner);
        if (allies.Count() > 0)
        {
            var highestHealthAlly = allies.Max(item => item.CurrentHp);
            action().DamageUnitNonAttack(allies.PickRandom(), null, 3);
        }

    }
}
