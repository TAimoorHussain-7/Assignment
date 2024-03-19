using UnityEngine;
using ProjectCore.Variables;
using Sirenix.OdinInspector;
using DG.Tweening;

[CreateAssetMenu(fileName = "SpinWheeAnimation", menuName = "ProjectCore/ScripAnimations/SpinWheelAnimation")]
public class SpinAnimtionSo : ScriptableObject
{
    public float SpinSpeed = 1f;
    public bool clockwise = false;
    public Ease SpinEase;
    public RotateType CurrentRotateType;

    [ShowIf("NotNormalSpin")]
    public float fastDuration;

    [ShowIf("CurrentRotateType", RotateType.Normal)]
    public float minSpinTime = 1f, maxSpinTime = 3f;

    private bool NotNormalSpin()
    {
        return CurrentRotateType == RotateType.StartFast || CurrentRotateType == RotateType.StartSlow;
    }
}
