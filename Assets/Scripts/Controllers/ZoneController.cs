using UnityEngine;
using UnityEngine.Events;

public class OnCurrZoneChanged : UnityEvent<int> { }

public class ZoneController : Singleton<ZoneController>
{
    [SerializeField] private InfiniteZoneSlider infiniteZoneSlider;

    private const int safeZoneModule = 5;
    public int SafeZoneModule => safeZoneModule;

    private const int superZoneModule = 10;
    public int SuperZoneModule => superZoneModule;

    public OnCurrZoneChanged OnCurrZoneChanged = new OnCurrZoneChanged();

    private int currZone;
    public int CurrZone
    {
        get => currZone;
        private set
        {
            currZone = value;
            CalculateZoneType(currZone);
            OnCurrZoneChanged.Invoke(currZone);
        }
    }

    private WheelZoneType currZoneType = WheelZoneType.Normal;
    public WheelZoneType CurrZoneType
    {
        get => currZoneType;
        set
        {
            currZoneType = value;
        }
    }

    private void Start()
    {
        CurrZone = 1;
        WheelController.Instance.FillWheel(CurrZone);
    }

    private void CalculateZoneType(int zone)
    {
        if (zone % superZoneModule == 0)
            CurrZoneType = WheelZoneType.Super;
        else if (zone % safeZoneModule == 0)
            CurrZoneType  = WheelZoneType.Safe;
        else
            CurrZoneType = WheelZoneType.Normal;
    }

    public void NextZone()
    {

        infiniteZoneSlider.ShiftLeft(() =>
        {
            CurrZone++;
            WheelController.Instance.FillWheel(currZone);
            if (WheelController.Instance.WheelData.willWonRewardId != RewardIDs.bomb)
                GameStateManager.Instance.SetState(GameStateManager.Instance.IdleState);

        });
    }

    public void Restart()
    {
        infiniteZoneSlider.Restart();
        CurrZone = 1;
        WheelController.Instance.FillWheel(CurrZone);
    }
}

public enum WheelZoneType
{
    Normal,
    Safe,
    Super
}