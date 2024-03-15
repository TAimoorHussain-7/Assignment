using UnityEngine;

namespace ProjectCore.Events
{
    [CreateAssetMenu(fileName = "e_", menuName = "ProjectCore/Events/Game Event With Int")]
    public class GameEventWithInt : GameEventWithParam<int>
    {
        public override void Raise(int t)
        {
            base.Raise(t);
        }
    }
}