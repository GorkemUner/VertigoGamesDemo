using UnityEngine;
using UnityEngine.Events;

namespace StateMachine.Core
{
    public abstract class State
    {
        public readonly UnityEvent OnEnter=new UnityEvent();
        public readonly UnityEvent OnExit=new UnityEvent();

        protected abstract void OnEntered();

        protected virtual void OnAfterEntered()
        {
        
        }

        protected abstract void OnExited();

        protected virtual void OnAfterExited()
        {
        
        }


        public void Enter()
        {
            OnEntered();
            OnEnter?.Invoke();
            OnAfterEntered();
            // Debug.Log("State Enter: "+this.GetType().Name);
        }

        public void Exit()
        {
            OnExited();
            OnExit?.Invoke();
            OnAfterExited();
            // Debug.Log("State Exit: "+this.GetType().Name);
        }
    }
}
