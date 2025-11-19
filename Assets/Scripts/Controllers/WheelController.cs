using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private Button spinBtn;

    private const int sliceCount = 8;
    public int SliceCount => sliceCount;

    private WheelData wheelData = new WheelData();
    public WheelData WheelData => wheelData;

    private void OnEnable()
    {
        spinBtn.onClick.AddListener(Spin);
        ZoneController.Instance.OnZoneTypeChange.AddListener(OnZoneTypeChange);
    }

    private void OnDisable()
    {
        spinBtn.onClick.RemoveListener(Spin);
        ZoneController.Instance.OnZoneTypeChange.RemoveListener(OnZoneTypeChange);
    }

    private void OnValidate()
    {
        spinBtn = GetComponentInChildren<Button>();
    }

    public void SpinBtnSetActive(bool isActive)
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

    public void Spin()
    {
        GameStateManager.Instance.SetState(GameStateManager.Instance.SpinningState);

        float rotation = (Random.Range(spinCountMin, spinCountMax) * 360) + (angleBetweenRewards * resultRewardIndex);
        wheelTransform.transform.DORotate(new Vector3(0, 0, rotation), rotateDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => OnSpinCompleted());
    }

    private void OnSpinCompleted()
    {
        if(wheelData.willWonRewardId == RewardIDs.bomb)
            GameStateManager.Instance.SetState(GameStateManager.Instance.BombState);

        PendingRewardsPanel.Instance.CreateItemWheelData(wheelData);
        ZoneController.Instance.NextZone();


    }

    private void OnZoneTypeChange(WheelZoneType wheelZoneType)
    {
        switch (wheelZoneType)
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
}