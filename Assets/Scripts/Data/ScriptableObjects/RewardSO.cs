using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Wheel/Reward")]
    public class RewardSO : ScriptableObject
    {
        public RewardData reward;
        public Sprite sprite;
    }

    [System.Serializable]
    public struct RewardData
    {
        public RewardIDs id;
        public int baseReward;
        public RewardData(RewardIDs id, int baseReward)
        {
            this.id = id;
            this.baseReward = baseReward;
        }
    }

    [System.Serializable]
    public enum RewardIDs
    {
        none,
        cash,
        chest_big,
        chest_Bronze,
        chest_gold,
        chest_silver,
        chest_small,
        chest_standard,
        chest_super,
        gold,
        bomb,
        neurostim,
        regenerator,
        armor_points,
        knife_points,
        pistol_points,
        rifle_points,
        shotgun_points,
        smg_points,
        sniper_points,
        submachine_points,
        vest_points,
        glass,
        cap,
        molotov,
        tier1_shotgun,
        pumpkin,
        bayonet_easter,
        bayonet_summer,
        tier2_mle,
        tier2_rifle,
        tier3_shotgun,
        tier3_smg,
        tier3_sniper
    }
}