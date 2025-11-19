
public class SpinState : IGameState
{
    public void Enter()
    {
        WheelController.Instance.SpinBtnSetActive(false);
        PendingRewardsPanel.Instance.CollectExitBtnSetState(false);
    }

    public void Exit()
    {
        WheelController.Instance.SpinBtnSetActive(true);
    }
}
