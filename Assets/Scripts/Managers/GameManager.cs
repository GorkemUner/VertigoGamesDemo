using Controllers;
using Other;
using Panel;
using StateMachine.States;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public static readonly StateMachine.Core.StateMachine StateMachine = new (
            new IdleState(),
            new SpinState(),
            new BombState(),
            new CollectState());
        public void Restart()
        {
            ZoneController.Instance.Restart();
            PendingRewardsPanel.Instance.Restart();
            StateMachine.EnterState<IdleState>();
        }
    }
}
