using UnityEngine;

[CreateAssetMenu(fileName = "SpinWheeAnimation", menuName = "ProjectCore/ScripAnimations/SpinWheelAnimation")]
public class SpinAnimtionSo : ScriptableObject
{
    public float minSpinTime = 1f;
    public float maxSpinTime = 3f;
    public float SpinSpeed = 1f;
    public bool clockwise = false;
}
