using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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

    public static List<T> Pull<T>(this List<T> list, int numberToPull)
    {
        System.Random random = new System.Random();
        if (list.Count < numberToPull)
            numberToPull = list.Count;
        List<T> newList = list.OrderBy(t => random.Next()).Take(numberToPull).ToList();
        return newList;
    }

    public static string RandomNumbers(int numberCount)
    {
        var stringBuilder = new StringBuilder();
        for (int i = 0; i < numberCount; i++)
        {
            stringBuilder.Append(Random.Range(0, 10));
        }
        return stringBuilder.ToString();
    }
}
