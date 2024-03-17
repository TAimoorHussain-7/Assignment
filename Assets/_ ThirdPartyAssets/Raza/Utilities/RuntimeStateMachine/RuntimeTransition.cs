using System.Collections;

namespace ProjectCore.RTStateMachine
{
    public class RuntimeTransition
    {
        public RuntimeState ToState;
        public RuntimeTransition(RuntimeState toState)
        {
            ToState = toState;
        }

        public virtual IEnumerator Start()
        {
            yield break;
        }
    }
}