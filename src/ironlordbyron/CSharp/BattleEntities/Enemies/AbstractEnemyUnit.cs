public abstract class AbstractEnemyUnit : AbstractBattleUnit
{
    public AbstractEnemyUnit()
    {
        this.IsAiControlled = true;
        this.IsAlly = false;
    }
}
