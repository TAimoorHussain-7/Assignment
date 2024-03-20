using UnityEngine;

namespace ProjectCore.Events
{
    [CreateAssetMenu(fileName = "e_", menuName = "ProjectCore/Events/Game Event With Int, String")]
    public class GameEventWithIntStringBool : GameEventWithParam<int, string,bool>
    {
        public override void Raise(int t, string u,bool v)
        {
            base.Raise(t, u,v);
        }
    }
}