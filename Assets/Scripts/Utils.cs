using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            (ts[r], ts[i]) = (ts[i], ts[r]);
        }
    }
}