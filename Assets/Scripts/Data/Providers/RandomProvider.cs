using Data.ScriptableObjects;
using Other;
using Strategy;
using UnityEngine;

namespace Data.Providers
{
    public class RandomProvider : Singleton<RandomProvider>, IWheelDataProvider
    {
        [SerializeField] private int priority = 50;

        [SerializeField] private BaseRandomStrategy strategy;

        [Space]
        [Header ("VertigoGamesStrategyAssets")]
        [SerializeField] public WheelSO normalZoneSO;
        [SerializeField] public WheelSO safeZoneSO;
        [SerializeField] public WheelSO superZoneSO;
        
        public int Priority => priority;
        public BaseRandomStrategy Strategy => strategy;
        
        private void OnEnable()
        {
            WheelResolver.Register(this);
        }

        public WheelData GetData(int zone)
        {
            return Strategy.Generate();
        }
        
    }
}