using Godot;
using System.Diagnostics;

public class Log
{
    public static void Info(string msg)
    {
        DebugInner(msg);
    }

    public static void Error(string msg, StackTrace st = null)
    {
        Err(msg, st);
    }

    public static void Err(string msg, StackTrace st)
    {
        GD.PrintErr($"{msg} [trace is {(st != null ? $"<color=red>{st.ToString()}</color>" : "")}]");
    }

    public static void DebugInner(string msg)
    {
        GD.Print(msg);
    }
}