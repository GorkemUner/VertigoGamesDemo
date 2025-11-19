using Managers;
using StateMachine.States;
using StateMachine.UI;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Panel
{
    public class BombPanel : BaseUIStatePanel
    {
        private Button restartBtn;
        private Button reviveBtn;
    
        private void OnValidate()
        {
            var btns = GetComponentsInChildren<Button>(true);
            restartBtn = btns[0];
            reviveBtn = btns[1];
        }

        private void OnEnable()
        {
            restartBtn.onClick.AddListener(Restart);
        }

        private void OnDisable()
        {
            restartBtn.onClick.RemoveListener(Restart);

        }

        private void Restart()
        {
            GameManager.Instance.Restart();
            transform.gameObject.SetActive(false);
        }

        protected override UnityEvent OnStateEnter() => GameManager.StateMachine.GetState<BombState>().OnEnter;

        protected override UnityEvent OnStateExit()=> GameManager.StateMachine.GetState<BombState>().OnExit;

        protected override bool InState() => GameManager.StateMachine.IsInState<BombState>();
    }
}
