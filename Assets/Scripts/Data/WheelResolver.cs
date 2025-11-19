using System.Collections.Generic;
using System.Linq;
using Controllers;
using Data.Providers;
using Data.ScriptableObjects;

namespace Data
{
    public static class WheelResolver
    {
        private static List<IWheelDataProvider> providers = new List<IWheelDataProvider>();
        public static WheelData wheelData;

        public static void Register(IWheelDataProvider provider)
        {
            if (!providers.Contains(provider))
                providers.Add(provider);

            providers = providers.OrderByDescending(p => p.Priority).ToList();
        }

        public static WheelData Resolve(int zone)
        {
            foreach (var p in providers)
            {
                wheelData = p.GetData(zone);
                if (IsDataAppropriate())
                    return wheelData;
            }

            return null;
        }

        private static bool IsDataAppropriate()
        {
            if ((wheelData == null)
                || (wheelData.willWonRewardId == RewardIDs.none)
                || (wheelData.rewards.Count != WheelController.Instance.SliceCount))
                return false;

            return true;
        }
    }
}