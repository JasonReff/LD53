using System.Collections.Generic;
using System.Linq;

public static class GameExtensions
{
    public static T GetNext<T>(this List<T> list, T currentItem)
    {
        var index = list.IndexOf(currentItem);
        if (index == list.Count - 1)
            return list[0];
        else
            return list[index + 1];
    }

    public static T GetPrev<T>(this List<T> list, T currentItem)
    {
        var index = list.IndexOf(currentItem);
        if (index == 0)
            return list[list.Count - 1];
        else
            return list[index - 1];
    }

    public static List<T> ShuffleAndCopy<T>(this List<T> list)
    {
        return list.OrderBy(t => UnityEngine.Random.value).ToList();
    }

    public static T Rand<T>(this IEnumerable<T> collection)
    {
        var random = new System.Random();
        return collection.OrderBy(t => random.Next()).First();
    }

    public static T Rand<T>(this IEnumerable<T> collection, System.Random random)
    {
        return collection.OrderBy(t => random.Next()).First();
    }
}
