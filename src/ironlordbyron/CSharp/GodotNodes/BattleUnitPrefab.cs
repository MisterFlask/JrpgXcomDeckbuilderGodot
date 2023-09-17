using System;

public class BattleUnitPrefab
{
    public object UnderlyingEntity { get; internal set; }

    internal void Initialize(AbstractBattleUnit abstractBattleUnit)
    {
        throw new NotImplementedException();
    }
}