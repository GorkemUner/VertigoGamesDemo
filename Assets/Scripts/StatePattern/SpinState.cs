using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinState : IGameState
{
    public void Enter()
    {
        WheelController.Instance.SpinBtnSetActive(false);
        //PendingRewardsPanel.Instance.CollectExitBtnSetState(false);
        //PendingRewardsPanel.Instance.CollectExitBtnSetState(ZoneController.Instance.GetCurrentZoneType() != WheelZoneType.Normal);
    }

    public void Exit()
    {
        WheelController.Instance.SpinBtnSetActive(true);
        //PendingRewardsPanel.Instance.CollectExitBtnSetState(ZoneController.Instance.GetCurrentZoneType() != WheelZoneType.Normal);
    }
}
