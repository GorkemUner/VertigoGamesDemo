using Data.Providers;
using UnityEngine;

namespace Strategy
{
    public abstract class BaseRandomStrategy : ScriptableObject
    {
        public abstract WheelData Generate();
    }
}