using UnityEngine;
public class GameStateManager : Singleton<GameStateManager>
{
    public IGameState CollectState { get; private set; }
    public IGameState SpinningState { get; private set; }
    public IGameState BombState { get; private set; }
    public IGameState IdleState { get; private set; }

    private IGameState currentState;

    private void Start()
    {
        CollectState = new CollectState();
        SpinningState = new SpinState();
        BombState = new BombState();
        IdleState = new IdleState();

        currentState = IdleState;
    }

    public void SetState(IGameState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
    }
}
