using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
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
}