using UnityEngine;

namespace ProjectCore.Events
{
    [CreateAssetMenu(fileName = "e_", menuName = "ProjectCore/Events/Game Event With String")]
    public class GameEventWithString : GameEventWithParam<string>
    {
        public override void Raise(string t)
        {
            base.Raise(t);
        }
    }
}

