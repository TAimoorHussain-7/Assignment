using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCore.StateMachine
{
    [CreateAssetMenu(fileName = "StateMachine", menuName = "ProjectCore/State Machine/Basic FSM")]
    public class FiniteStateMachine : ScriptableObject, IState
    {
        [SerializeField] private State BootState;

        [ShowInInspector] [NonSerialized] protected State CurrentState;
        [ShowInInspector] [NonSerialized] protected State PreviousState;
        [ShowInInspector] [NonSerialized] protected Transition CurrentTransition;
        [ShowInInspector] [NonSerialized] protected bool ResumePreviousState = false;

        [NonSerialized] private Stack<State> PausedStates = new Stack<State>();

        public State RunningState
        {
            get
            {
                return CurrentState;
            }
        }

        public bool IsStatePaused(State state)
        {
            return PausedStates.Contains(state);
        }

        void IState.TransitionTo(State state, Transition transition)
        {
            Transition(transition);
        }

        IEnumerator IState.CleanupAllPausedStates(State state)
        {
            if (state == CurrentState)
            {
                while (PausedStates.Count > 0)
                {
                    yield return PausedStates.Pop().Cleanup();
                }
            }
        }

        public void ShouldResumePreviousState ()
        {
            if(PausedStates.Count ==0) return;
            
            ResumePreviousState = true;
        }

        public void Transition (Transition transition)
        {
            //Debug.LogError($"Transitioning To: {transition.ToState.name}");
            var canTransition = CurrentTransition == null && transition != null && transition.ToState != null;
            if (canTransition)
            {
                CurrentTransition = transition;
            }
        }

        public IEnumerator Tick ()
        {
            if (CurrentState == null)
            {
                CurrentState = BootState;
                yield return CurrentState.Init(this);
                yield return CurrentState.Execute();
            }

            while (true)
            {
                if (CurrentTransition != null && CurrentTransition.ToState != null)
                {
                    if (CurrentTransition.ToState.PausesPreviousState)
                    {
                        PausedStates.Push(CurrentState);
                        yield return CurrentState.Pause();
                    }
                    else
                    {
                        yield return CurrentState.Exit();
                    }

                    yield return CurrentTransition.Execute();
                   
                    SetState(CurrentTransition.ToState);

                    yield return CurrentState.Init(this);
                    yield return CurrentState.Execute();
                    CurrentTransition = null;
                }
                else if (ResumePreviousState && PausedStates.Count >0)
                {
                    yield return CurrentState.Exit();
                    SetState(PausedStates.Pop());
                    yield return CurrentState.Resume();
                    ResumePreviousState = false;
                }

                yield return CurrentState.Tick();
                yield return null;
            }
        }

        private void SetState(State state)
        {
            PreviousState = CurrentState;
            CurrentState = state;
        }
    }
}

