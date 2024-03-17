using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCore.StateMachine
{
    [CreateAssetMenu(fileName = "Transition", menuName = "ProjectCore/State Machine/Transitions/Basic Transition")]
    public class Transition : ScriptableObject
    {
        public State ToState;

        public virtual IEnumerator Execute ()
        {
            yield break;
        }
    }
}
