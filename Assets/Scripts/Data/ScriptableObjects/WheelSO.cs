using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Wheel/WheelConfig")]
    public class WheelSO : ScriptableObject
    {
        public WheelZoneType type;
        public List<RewardSO> MustRewards;
        public List<RewardSO> PreferredRewards;
        
    }
}