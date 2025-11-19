using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackendProvider : MonoBehaviour, IWheelDataProvider
{
    [SerializeField] private string URL;
    [SerializeField] private int priority = 100;

    private List<WheelData> backendData;
    public int Priority => priority;

    private void OnEnable()
    {
        WheelResolver.Register(this);
        SendRequest();
    }

    public void SendRequest()
    {
        BackendDummy.Instance.SendRequest(this, URL);
    }

    public void Response(List<WheelData> wheelData)
    {
        backendData = wheelData;
    }

    public WheelData GetData(int zone)
    {
        return backendData.FirstOrDefault(x=>x.zone == zone);
    }
}