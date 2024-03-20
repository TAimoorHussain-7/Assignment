using UnityEngine;

namespace ProjectCore.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "ProjectCore/Variables/SpinData")]
    public class SpinDataSO : ScriptableObject
    {
        public SpinWheelData SpData;
    }
}
