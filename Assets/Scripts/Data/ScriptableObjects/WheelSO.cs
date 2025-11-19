using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wheel/WheelConfig")]
public class WheelSO : ScriptableObject
{
    public WheelZoneType type;
    public List<RewardSO> MustRewards;
    public List<RewardSO> PreferredRewards;

    private int maxSize = WheelController.Instance.SliceCount;
    private void OnValidate()
    {
        if (MustRewards.Count > maxSize)
        {
            Debug.LogWarning($"The list can contain at most {maxSize} items!");
            while (MustRewards.Count > maxSize)
                MustRewards.RemoveAt(MustRewards.Count - 1);
        }
    }
}