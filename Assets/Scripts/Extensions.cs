using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static int IndexOf<T>(this IEnumerable<T> collection, T searchItem)
    {
        int index = 0;

        foreach (var item in collection)
        {
            if (EqualityComparer<T>.Default.Equals(item, searchItem))
            {
                return index;
            }

            index++;
        }

        return -1;
    }
}
