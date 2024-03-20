using UnityEngine;
using System.Collections;

namespace ProjectCore.RTStateMachine
{
    public class RuntimeStateMachine : MonoBehaviour, IRuntimeState
    {
        protected RuntimeState _currentState;

        RuntimeState _bootState;
        RuntimeState _previousState;
        RuntimeTransition _currentTransition;

        Coroutine _stateMachineRoutine; 

        public virtual void StartStateMachine(RuntimeState state)
        {
            _bootState = state;
            _stateMachineRoutine = StartCoroutine(Tick());
        }

        public void Transition(RuntimeTransition transition)
        {
            _currentTransition = transition;
        }

        void IRuntimeState.TransitionTo(RuntimeState state, RuntimeTransition transition)
        {
            Transition(transition);
        }

        protected void SetState (RuntimeState state)
        {
            _previousState = _currentState;
            _currentState = state;
        }

        private IEnumerator Tick()
        {
            while (true)
            {
                if (_currentState == null)
                {
                    _currentState = _bootState;
                    if (_currentState != null)
                    {
                        yield return _currentState.Start();
                    }
                }
                else
                {
                    if (_currentTransition != null && _currentTransition.ToState != null)
                    {
                        yield return _currentState.Exit();

                        yield return _currentTransition.Start();
                        SetState(_currentTransition.ToState);
                        _currentTransition = null;

                        yield return _currentState.Start();

                    }
                    yield return _currentState.Update();
                }
                yield return null;
            }
        }
    }
}