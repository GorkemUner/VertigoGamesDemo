using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace StateMachine.Core
{
    public class StateMachine
    {
        private State _previous;
        private State _current;
        private Dictionary<Type,State> _states;
        public readonly UnityEvent OnStateChanged = new UnityEvent();

        public State Current => _current;

        public State Previous => _previous;

        public bool IsInState<T>() where T : State => _current is T;
        
        
        public StateMachine(params State[] states)
        {
            _states = new Dictionary<Type, State>();
            if(states!=null && states.Length>0)
                foreach (var state in states)
                {
                    if(!_states.ContainsKey(state.GetType()))
                        _states.Add(state.GetType(),state);
                    else
                    {
                        throw new Exception("StateMachine init error: All states must be unique. This state already added" + state.GetType().Name);
                    }
                }
        }
        
        public void EnterState<T>() where T : State
        {
            var newState = GetState<T>();
            EnterState(newState);
        }

        public State GetState<T>() where T : State
        {
            if (_states.TryGetValue(typeof(T),out var newState))
            {
                return newState;
            }
            throw new Exception("StateMachine.GetState error : There is no registered state as T:" + typeof(T).Name);
        }

        private void EnterState(State newState)
        {
            if (_current != null && _current.Equals(newState)) return;
            _previous = _current;
            _current = newState;
            _previous?.Exit();
            _current.Enter();
            OnStateChanged?.Invoke();
        }
    }
}
