using System.Collections.Generic;
using UnityEngine;

public class GameDesignerProvider : MonoBehaviour, IWheelDataProvider
{
    [SerializeField] private int priority = 50;
    [SerializeField] private List<WheelData> wheelData;
    public int Priority => priority;

    private void OnEnable()
    {
        WheelResolver.Register(this);
    }

    public WheelData GetData(int zone)
    {
        if (zone >= wheelData.Count)
            return null;

        return wheelData[zone];
    }
}
