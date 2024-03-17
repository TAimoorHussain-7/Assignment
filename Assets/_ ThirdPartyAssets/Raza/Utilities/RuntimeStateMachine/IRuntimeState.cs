namespace ProjectCore.RTStateMachine
{
    public interface IRuntimeState
    {
        void TransitionTo(RuntimeState state, RuntimeTransition transition);
    }
}