using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnZoneTypeChange : UnityEvent<WheelZoneType> { }

public class ZoneController : Singleton<ZoneController>
{
    private const int safeZoneModule = 5;
    public int SafeZoneModule => safeZoneModule;

    private const int superZoneModule = 10;
    public int SuperZoneModule => superZoneModule;

    private int currZone = 1;
    public int CurrZone
    {
        get => currZone;
        private set
        {
            currZone = value;
            CalculateZoneType(currZone);
            infiniteZoneSlider.ShiftLeft(() => WheelController.Instance.FillWheel(currZone));
        }
    }

    public OnZoneTypeChange OnZoneTypeChange = new OnZoneTypeChange();
    private WheelZoneType currZoneType = WheelZoneType.Normal;
    public WheelZoneType CurrZoneType
    {
        get => currZoneType;
        set
        {
            currZoneType = value;
            OnZoneTypeChange.Invoke(currZoneType);
        }
    }

    [SerializeField] private InfiniteZoneSlider infiniteZoneSlider;
    private void Start()
    {
        CalculateZoneType(currZone);
        WheelController.Instance.FillWheel(CurrZone);
    }

    public void CalculateZoneType(int zone)
    {
        if (zone % superZoneModule == 0)
            CurrZoneType = WheelZoneType.Super;
        else if (zone % safeZoneModule == 0)
            CurrZoneType  = WheelZoneType.Safe;
        else
            CurrZoneType = WheelZoneType.Normal;
    }

    public void Next()
    {
        CurrZone++;
    }

    public void Restart()
    {
        infiniteZoneSlider.Restart();
        currZone = 1;
        WheelController.Instance.FillWheel(CurrZone);
    }
}

public enum WheelZoneType
{
    Normal,
    Safe,
    Super
}