using System.Collections;

namespace ProjectCore.RTStateMachine
{
    public class RuntimeState
    {
        protected IRuntimeState _listener;

        public RuntimeState(IRuntimeState listener)
        {
            _listener = listener;
        }

        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator Update()
        {
            yield break;
        }

        public virtual IEnumerator Exit()
        {
            yield break;
        }
    }
}