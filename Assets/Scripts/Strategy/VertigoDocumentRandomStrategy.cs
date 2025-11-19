using Controllers;
using Data.Providers;
using Data.ScriptableObjects;
using UnityEngine;
using Utils;

namespace Strategy
{
    [CreateAssetMenu(menuName = "Game/VertigoDocumentRandomStrategy")]
    public class VertigoDocumentRandomStrategy : BaseRandomStrategy
    {
        public override WheelData Generate()
        {
            var currZoneType = ZoneController.Instance.CurrZoneType;
            var currZone = ZoneController.Instance.CurrZone;
            WheelData result = new WheelData();

            WheelSO wheelSOActive = null;
            switch (currZoneType)
            {
                case WheelZoneType.Normal:
                    wheelSOActive = RandomProvider.Instance.normalZoneSO;
                    break;
                case WheelZoneType.Safe:
                    wheelSOActive = RandomProvider.Instance.safeZoneSO;
                    break;
                case WheelZoneType.Super:
                    wheelSOActive = RandomProvider.Instance.superZoneSO;
                    break;
            }

            foreach (var item in wheelSOActive.MustRewards)
            {
                result.rewards.Add(new RewardData(item.reward.id, item.reward.baseReward * currZone));
            }

            while (result.rewards.Count < WheelController.Instance.SliceCount)
            {
                var rand = Random.Range(0, wheelSOActive.PreferredRewards.Count);
                var id = wheelSOActive.PreferredRewards[rand].reward.id;
                var rewardAmount = wheelSOActive.PreferredRewards[rand].reward.baseReward * currZone;
                result.rewards.Add(new RewardData(id, rewardAmount));
            }

            var randd = Random.Range(0, result.rewards.Count);
            result.willWonRewardId = result.rewards[randd].id;

            MathUtil.Shuffle(result.rewards);


            return result;
        }
    }
}