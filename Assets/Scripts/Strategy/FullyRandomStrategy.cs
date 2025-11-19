using Controllers;
using Data.Providers;
using Data.ScriptableObjects;
using UnityEngine;

namespace Strategy
{
    [CreateAssetMenu(menuName = "Game/FullyRandomStrategy")]
    public class FullyRandomStrategy : BaseRandomStrategy
    {
        public override WheelData Generate()
        {
            WheelData result = new WheelData();

            while (result.rewards.Count < WheelController.Instance.SliceCount)
            {
                var randRewardId = (RewardIDs)Random.Range(1, System.Enum.GetNames(typeof(RewardIDs)).Length);
                var randRewardAmount = Random.Range(1, 1000);

                result.rewards.Add(new RewardData(randRewardId, randRewardAmount));
            }
            var randd = Random.Range(0, result.rewards.Count);
            result.willWonRewardId = result.rewards[randd].id;
            return result;
        }
    }
}