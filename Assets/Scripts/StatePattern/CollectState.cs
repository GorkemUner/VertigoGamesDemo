
public class CollectState : IGameState
{
    public void Enter()
    {
        GameManager.Instance.CollectPanel.SetActive(true);
    }

    public void Exit()
    {
        GameManager.Instance.CollectPanel.SetActive(false);
    }
}
