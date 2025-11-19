using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Providers;
using Data.ScriptableObjects;
using DG.Tweening;
using Managers;
using Other;
using Panel;
using StateMachine.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class WheelController : Singleton<WheelController>
    {
        [SerializeField] private Transform wheelTransform;
        [SerializeField] private Sprite normalRollerSprite;
        [SerializeField] private Sprite normalPinSprite;
        [SerializeField] private Sprite safeRollerSprite;
        [SerializeField] private Sprite safePinSprite;
        [SerializeField] private Sprite superRollerSprite;
        [SerializeField] private Sprite superPinSprite;
        [SerializeField] private Image rollerImage;
        [SerializeField] private Image pinImage;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descText;
        [SerializeField] private Color normalColor;
        [SerializeField] private Color safeColor;
        [SerializeField] private Color superColor;
        [SerializeField] private List<RewardItem> rewardItems;

        private int spinCountMin = 2, spinCountMax = 5;
        private int resultRewardIndex;
        private const float angleBetweenRewards = 45;
        private const float rotateDuration = 2f;
        private const float fullRotation = 360f;
        private Button spinBtn;

        private const int sliceCount = 8;
        public int SliceCount => sliceCount;

        private WheelData wheelData = new WheelData();
        public WheelData WheelData => wheelData;

        private void OnValidate()
        {
            spinBtn = GetComponentInChildren<Button>();
        }
    
        private void OnEnable()
        {
            spinBtn.onClick.AddListener(Spin);
            ZoneController.Instance.OnCurrZoneChanged.AddListener(OnCurrZoneChanged);
            GameManager.StateMachine.GetState<IdleState>().OnEnter.AddListener(OnIdleEnter);
            GameManager.StateMachine.GetState<IdleState>().OnExit.AddListener(OnIdleExit);
            GameManager.StateMachine.GetState<SpinState>().OnEnter.AddListener(OnSpinEnter);
            GameManager.StateMachine.GetState<SpinState>().OnExit.AddListener(OnSpinExit);
        }
        private void OnDisable()
        {
            spinBtn.onClick.RemoveListener(Spin);
            ZoneController.Instance.OnCurrZoneChanged.RemoveListener(OnCurrZoneChanged);
            GameManager.StateMachine.GetState<IdleState>().OnEnter.RemoveListener(OnIdleEnter);
            GameManager.StateMachine.GetState<IdleState>().OnExit.RemoveListener(OnIdleExit);
            GameManager.StateMachine.GetState<SpinState>().OnEnter.AddListener(OnSpinEnter);
            GameManager.StateMachine.GetState<SpinState>().OnExit.AddListener(OnSpinExit);
        }
    
        private void OnSpinEnter()
        {
            SpinBtnSetActive(false);
        }
        private void OnSpinExit()
        {
            SpinBtnSetActive(true);
        }
    
        private void OnIdleEnter()
        {
            SpinBtnSetActive(true);
        }

        private void OnIdleExit()
        {
            SpinBtnSetActive(false);
        }

        private void SpinBtnSetActive(bool isActive)
        {
            spinBtn.interactable = isActive;
        }

        public void FillWheel(int zone)
        {
            wheelData = WheelResolver.Resolve(zone);
            resultRewardIndex = wheelData.rewards.IndexOf(wheelData.rewards.First(x => x.id == wheelData.willWonRewardId));
            for (int i = 0; i < wheelData.rewards.Count; i++)
                rewardItems[i].Fill(ClientItemDatabase.Instance.GetItem((int)wheelData.rewards[i].id), wheelData.rewards[i].baseReward);
        }

        private void Spin()
        {
            GameManager.StateMachine.EnterState<SpinState>();
            float rotation = (Random.Range(spinCountMin, spinCountMax) * fullRotation) + (angleBetweenRewards * resultRewardIndex);
            wheelTransform.transform.DORotate(new Vector3(0, 0, rotation), rotateDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutCubic)
                .SetId(this)
                .OnComplete(() => OnSpinCompleted());
        }

        private void OnSpinCompleted()
        {
            if(wheelData.willWonRewardId == RewardIDs.bomb)
                GameManager.StateMachine.EnterState<BombState>();


            PendingRewardsPanel.Instance.CreateItemWheelData(wheelData);
            ZoneController.Instance.NextZone();
        }

        private void OnCurrZoneChanged(int currZone)
        {
            switch (ZoneController.Instance.CurrZoneType)
            {
                case WheelZoneType.Normal:
                    SetRollerAndPin(normalRollerSprite,normalPinSprite,"NORMAL SPIN", "Basic Rewards", normalColor);
                    break;
                case WheelZoneType.Safe:
                    SetRollerAndPin(safeRollerSprite, safePinSprite, "SAFE SPIN", "Rare Rewards", safeColor);
                    break;
                case WheelZoneType.Super:
                    SetRollerAndPin(superRollerSprite, superPinSprite, "SUPER SPIN", "Special Rewards", superColor);
                    break;
            }
        }

        private void SetRollerAndPin(Sprite roller, Sprite pin, string title, string desc, Color color)
        {
            rollerImage.sprite = roller;
            pinImage.sprite = pin;
            titleText.text = title;
            descText.text = desc;
            titleText.color = color;
            descText.color = color;
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }
    }
}