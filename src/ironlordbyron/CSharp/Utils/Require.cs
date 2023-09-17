public static class Require
{

    public static void NotNull(object obj)
    {
        if (obj == null)
        {
            throw new System.Exception("Object could not be null.");
        }
    }
}
