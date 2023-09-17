using System.Collections;
using System.Collections.Generic;

public abstract class AbstractAllyUnit : AbstractBattleUnit
{
    public AbstractAllyUnit()
    {
        this.IsAlly = true;
        this.IsAiControlled = false;
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        return new List<AbstractIntent>();
    }

    public abstract List<AbstractCard> CardsSelectableOnLevelUp();
}
