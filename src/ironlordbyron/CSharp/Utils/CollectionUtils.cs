using System.Collections.Generic;

public static class CollectionUtils
{
    public static IEnumerable<T> Aggregate<T>(params IEnumerable<T>[] enumerables)
    {
        var retval = new List<T>();
        foreach (var item in enumerables)
        {
            retval.AddRange(item);
        }
        return retval;
    }

}
