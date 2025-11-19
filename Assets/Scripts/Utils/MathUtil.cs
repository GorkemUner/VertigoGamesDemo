using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MathUtil
{
    //Fisher-Yates Shuffle
    public static void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randIndex = Random.Range(i, list.Count);
            (list[i], list[randIndex]) = (list[randIndex], list[i]);
        }
    }
}