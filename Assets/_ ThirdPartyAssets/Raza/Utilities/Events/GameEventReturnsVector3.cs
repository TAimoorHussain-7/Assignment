using UnityEngine;

namespace ProjectCore.Events
{
    [CreateAssetMenu(fileName = "e_", menuName = "ProjectCore/Events/Game Event Returns Vector3")]
    public class GameEventReturnsVector3 : GameEventWithReturn<Vector3?>
    {
        public override Vector3? Raise()
        {
            try
            {
                return base.Raise();
            }
            catch
            {
                return null;
            }
        }
    }
}
