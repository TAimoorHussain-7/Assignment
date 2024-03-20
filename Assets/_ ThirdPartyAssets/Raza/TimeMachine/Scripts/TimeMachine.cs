using System.Collections;
using ProjectCore.Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectCore.TimeMachine
{
    [CreateAssetMenu(fileName = "TimeMachine", menuName = "ProjectCore/TimeMachine")]
    public class TimeMachine : ScriptableObject
    {
        [FormerlySerializedAs("OnTick")]
        [Header("Events Raised")]
        [SerializeField] private GameEvent TickEvent;
        private readonly WaitForSeconds _waitForOneSecond = new WaitForSeconds(1);

        public IEnumerator Tick()
        {
            while (true)
            {
                yield return _waitForOneSecond;
                TickEvent.Invoke();
            }
        }
    }
}
