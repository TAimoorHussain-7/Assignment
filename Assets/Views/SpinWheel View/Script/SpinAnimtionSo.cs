using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "SpinWheeAnimation", menuName = "ProjectCore/ScripAnimations/SpinWheelAnimation")]
public class SpinAnimtionSo : ScriptableObject
{
    public int SpinRounds = 10;
    public bool clockwise = false;
    public Ease SpinEase;
    public float minSpinTime = 1f, maxSpinTime = 3f;
}
