using UnityEngine;

namespace ProjectCore.Events
{
    public delegate T GameEventHandlerWithReturn<T>();

    public class GameEventWithReturn<T> : ScriptableObject
    {
        public event GameEventHandlerWithReturn<T> Handler;
        public virtual T Raise()
        {
            return Handler.Invoke();
        }
    }
}
