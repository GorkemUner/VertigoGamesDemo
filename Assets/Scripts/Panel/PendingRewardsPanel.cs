using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PendingRewardsPanel : Singleton<PendingRewardsPanel>
{
    [SerializeField] private GameObject pendingItemGO;
    [SerializeField] private Transform contentTransform;
    [SerializeField] private Button collectExitBtn;

    private Dictionary<RewardIDs, PendingRewardItem> rewardIdItem = new Dictionary<RewardIDs, PendingRewardItem>();

    public ScrollRect scrollRect;

    private void OnEnable()
    {
        collectExitBtn.interactable = ZoneController.Instance.CurrZoneType != WheelZoneType.Normal;
        collectExitBtn.onClick.AddListener(OnClickCollectExit);
    }

    private void OnDisable()
    {
        collectExitBtn.onClick.RemoveListener(OnClickCollectExit);
    }

    private void OnClickCollectExit()
    {
        GameStateManager.Instance.SetState(GameStateManager.Instance.CollectState);

    }

    public void CollectExitBtnSetState(bool state)
    {

        collectExitBtn.interactable = state;
    }

    public void CreateItemWheelData(WheelData wheelData)
    {
        if (wheelData.willWonRewardId == RewardIDs.bomb)
            return;

        var sprite = ClientItemDatabase.Instance.GetItem((int)wheelData.willWonRewardId);
        var amount = wheelData.rewards[wheelData.rewards.IndexOf(wheelData.rewards.First(x => x.id == wheelData.willWonRewardId))].baseReward;

        if (rewardIdItem.ContainsKey(wheelData.willWonRewardId))
        {
            rewardIdItem[wheelData.willWonRewardId].AddAmount(amount);
            return;
        }

        PendingRewardItem pendingRewardItem = Instantiate(pendingItemGO, contentTransform).GetComponent<PendingRewardItem>();
        pendingRewardItem.Fill(sprite, amount);
        rewardIdItem.Add(wheelData.willWonRewardId, pendingRewardItem);
        ScrollToBottom();
    }


    void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases();
        if (scrollRect.verticalNormalizedPosition > 0)
            scrollRect.verticalNormalizedPosition = 0f;
    }

    public void Restart()
    {
        List<GameObject> gos = new List<GameObject>();
        foreach (var item in rewardIdItem)
        {
            gos.Add(item.Value.gameObject);
        }

        foreach (var go in gos)
        {
            Destroy(go);
        }
        rewardIdItem.Clear();
        collectExitBtn.interactable = ZoneController.Instance.CurrZoneType != WheelZoneType.Normal;
    }
}