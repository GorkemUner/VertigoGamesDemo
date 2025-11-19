using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject CollectPanel;
    public GameObject BombPanel;

    public void Restart()
    {
        ZoneController.Instance.Restart();
        PendingRewardsPanel.Instance.Restart();
        GameStateManager.Instance.SetState(GameStateManager.Instance.IdleState);
    }
}
