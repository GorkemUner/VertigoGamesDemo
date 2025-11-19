using TMPro;
using UnityEngine;

public class ZoneInfoPanel : MonoBehaviour
{
    private int target;
    private int increaseAmount;
    TextMeshProUGUI info;
    [SerializeField] private WheelZoneType targetZoneType;

    private void OnValidate()
    {
        info = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        ZoneController.Instance.OnCurrZoneChanged.AddListener(OnCurrZoneChanged);
    }

    private void OnDisable()
    {
        ZoneController.Instance.OnCurrZoneChanged.RemoveListener(OnCurrZoneChanged);
    }

    private void Start()
    {
        switch (targetZoneType)
        {
            case WheelZoneType.Safe:
                target = ZoneController.Instance.SafeZoneModule;
                info.text = target.ToString();
                break;
            case WheelZoneType.Super:
                target = ZoneController.Instance.SuperZoneModule;
                info.text = target.ToString();
                break;
        }
        increaseAmount = target;
    }

    private void OnCurrZoneChanged(int currZone)
    {
        target = ((currZone / (targetZoneType == WheelZoneType.Safe ? ZoneController.Instance.SafeZoneModule : ZoneController.Instance.SuperZoneModule) + 1)) * increaseAmount;

        if (targetZoneType == WheelZoneType.Safe && ((target % ZoneController.Instance.SuperZoneModule) == 0))
            target += increaseAmount;

        info.text = target.ToString();
    }
}
