using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class ListExtentions
{
    public static T RemoveRandom<T>(this List<T> list)
    {
        var randomIndex = Random.Range(0, list.Count);
        var item = list[randomIndex];

        list.RemoveAt(randomIndex);
        return item;
    }
}
