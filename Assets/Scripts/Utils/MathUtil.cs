using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MathUtil
{
    public static int RandomRangeExcept(int minInclusive, int maxExclusive, List<int> except = null)
    {
        IEnumerable<int> allowedRandomNumbers = Enumerable.Range(minInclusive, maxExclusive - minInclusive);

        if (except != null)
            allowedRandomNumbers = allowedRandomNumbers.Except(except);

        if (allowedRandomNumbers.Count() == 0)
        {
            Debug.LogError("Allowed numbers array is empty! Returning 0");
            return 0;
        }
        int randomIndex = UnityEngine.Random.Range(0, allowedRandomNumbers.Count());
        return allowedRandomNumbers.ElementAt(randomIndex);
    }

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