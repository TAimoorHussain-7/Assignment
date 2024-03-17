using System.Collections;

namespace ProjectCore.StateMachine
{
    public interface IState
    {
        void TransitionTo(State state, Transition transition);
        IEnumerator CleanupAllPausedStates(State state);
    }
}