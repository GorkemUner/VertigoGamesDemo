using Managers;
using StateMachine.States;
using StateMachine.UI;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Panel
{
    public class CollectedPanel : BaseUIStatePanel
    {
        private Button restartBtn;

        private void OnValidate()
        {
            restartBtn = GetComponentInChildren<Button>(true);
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
            HidePanel();
        }

        private void HidePanel()
        {
            transform.gameObject.SetActive(false);
        }

        protected override UnityEvent OnStateEnter() => GameManager.StateMachine.GetState<CollectState>().OnEnter;

        protected override UnityEvent OnStateExit()=> GameManager.StateMachine.GetState<CollectState>().OnExit;

        protected override bool InState() => GameManager.StateMachine.IsInState<CollectState>();
    }
}
