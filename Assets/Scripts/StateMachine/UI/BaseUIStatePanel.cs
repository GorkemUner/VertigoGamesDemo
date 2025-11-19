using UnityEngine;
using UnityEngine.Events;

namespace StateMachine.UI
{
    public abstract class BaseUIStatePanel: MonoBehaviour
    {
        protected abstract UnityEvent OnStateEnter();
        protected abstract UnityEvent OnStateExit();

        protected abstract bool InState();

        protected virtual void Awake()
        {
            OnStateEnter().AddListener(OnEnter);
            OnStateExit().AddListener(OnExit);
            gameObject.SetActive(InState());
        }
        protected virtual void OnDestroy()
        {
            OnStateEnter()?.RemoveListener(OnEnter);
            OnStateExit()?.RemoveListener(OnExit);
        }

        protected virtual  void OnExit()
        {
            SetEnabled(false);
        }

        protected virtual void OnEnter()
        {
            SetEnabled(true);
        }
    
        protected virtual void SetEnabled(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}