using System.Collections.Generic;

public interface IWheelDataProvider
{
    public int Priority { get; }
    public WheelData GetData(int zone);
}

[System.Serializable]
public class WheelData
{
    public int zone;
    public List<RewardData> rewards = new List<RewardData>();
    public RewardIDs willWonRewardId;

    public WheelData(int zone = 0, List<RewardData> rewards= null, RewardIDs willWonRewardId = RewardIDs.none)
    {
        this.zone = zone;
        this.rewards = new List<RewardData>();
        this.willWonRewardId = willWonRewardId;
    }
}