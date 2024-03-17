using System;
using System.Collections;
using UnityEngine;

namespace ProjectCore.StateMachine
{
    public class State : ScriptableObject
    {
        [SerializeField] public bool PausesPreviousState;
        [NonSerialized] public bool Paused;

        protected IState _Listener;
        
        //When the State is Initialized
        public virtual IEnumerator Init(IState listener)
        {
            _Listener = listener;
            yield break;
        }

        //when the state starts execution
        public virtual IEnumerator Execute()
        {
            yield break;
        }

        //update the state
        public virtual IEnumerator Tick()
        {
            yield break;
        }

        //state will end
        public virtual IEnumerator Exit()
        {
            yield break;
        }

        public virtual IEnumerator Resume()
        {
            Paused = false;
            yield break;
        }

        public virtual IEnumerator Pause()
        {
            Paused = true;
            yield break;
        }

        public virtual IEnumerator Cleanup()
        {
            Paused = false;
            yield break;
        }
    }
}
