using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IGameState
{
    public void Enter()
    {
        WheelController.Instance.SpinBtnSetActive(true);
        PendingRewardsPanel.Instance.CollectExitBtnSetState(ZoneController.Instance.CurrZoneType != WheelZoneType.Normal);
    }

    public void Exit()
    {
        WheelController.Instance.SpinBtnSetActive(false);
    }
}