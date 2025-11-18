using TMPro;
using UnityEngine;

public class ZoneInfoPanel : MonoBehaviour
{
    private int target;
    private int increaseAmount;
    TextMeshProUGUI info;
    [SerializeField] private WheelZoneType targetZoneType;

    #if UNITY_EDITOR
    private void OnValidate()
    {
        info = GetComponentInChildren<TextMeshProUGUI>();
    }
    #endif

    private void OnEnable()
    {
        ZoneController.Instance.OnZoneTypeChange.AddListener(OnZoneTypeChange);
    }

    private void OnDisable()
    {
        ZoneController.Instance.OnZoneTypeChange.RemoveListener(OnZoneTypeChange);
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

    private void OnZoneTypeChange(WheelZoneType zoneType)
    {
        if (zoneType == targetZoneType)
        {
            target += increaseAmount;
            if(targetZoneType == WheelZoneType.Safe && (target % ZoneController.Instance.SuperZoneModule == 0))
                target += increaseAmount;
            info.text = target.ToString();
        }
    }
}
