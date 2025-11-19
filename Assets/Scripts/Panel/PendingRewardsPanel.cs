using System.Collections.Generic;
using System.Linq;
using Controllers;
using Data;
using Data.Providers;
using Data.ScriptableObjects;
using Managers;
using Other;
using StateMachine.States;
using UnityEngine;
using UnityEngine.UI;

namespace Panel
{
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
            GameManager.StateMachine.GetState<IdleState>().OnEnter.AddListener(OnIdleEnter);
            GameManager.StateMachine.GetState<SpinState>().OnEnter.AddListener(OnSpinEnter);
        }



        private void OnDisable()
        {
            collectExitBtn.onClick.RemoveListener(OnClickCollectExit);
            GameManager.StateMachine.GetState<IdleState>().OnEnter.RemoveListener(OnIdleEnter);
            GameManager.StateMachine.GetState<SpinState>().OnEnter.RemoveListener(OnSpinEnter);
        
        }

        private void OnClickCollectExit()
        {
            GameManager.StateMachine.EnterState<CollectState>();
        }
    
        private void OnSpinEnter()
        {
            CollectExitBtnSetState(false);

        }

        private void OnIdleEnter()
        {
            CollectExitBtnSetState(ZoneController.Instance.CurrZoneType != WheelZoneType.Normal); 

        }

        private void CollectExitBtnSetState(bool state)
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
}