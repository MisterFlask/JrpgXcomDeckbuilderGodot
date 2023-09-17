using System;

public class ServiceLocator
{
    public static GameState _GameState = new GameState();
    public static GameState GameState()
    {
        return _GameState;
    }

    internal static ActionManager GetActionManager()
    {
        throw new NotImplementedException();
    }
}