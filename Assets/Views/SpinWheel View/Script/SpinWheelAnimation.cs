using DG.Tweening;
using ProjectCore.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheelAnimation : SpinWheel
{
    [SerializeField] Button SpinButton;
    [SerializeField] Transform wheelTransform;
    public float minSpinTime = 1f;
    public float maxSpinTime = 3f;
    public float SpinSpeed = 1f;
    public bool clockwise = true;

    private void OnEnable()
    {
        SpinButton.onClick.AddListener(OnSpinWheel);
    }

    private void OnDisable()
    {
        SpinButton.onClick.RemoveListener(OnSpinWheel);
    }

    [Button]
    private void OnSpinWheel()
    {
        if (!IsSpining)
        {
            IsSpining = true;
            SpinButton.enabled = false;

            float targetDirection = clockwise ? 360f : -360f;
            float totalProbability = 0f;

            foreach (RewardData reward in SpData.SpData.rewards)
            {
                totalProbability += reward.probability;
            }

            float randomValue = Random.Range(0f, totalProbability);

            float currentProbability = 0f;
            for (int i = 0; i < SpData.SpData.rewards.Length; i++)
            {
                currentProbability += SpData.SpData.rewards[i].probability;

                if (randomValue <= currentProbability)
                {
                    float targetRotation = targetDirection * (1f - (float)i / SpData.SpData.rewards.Length);
                    targetRotation *= SpinSpeed;
                    float spinTime = Random.Range(minSpinTime, maxSpinTime);

                    wheelTransform.DORotate(new Vector3(0f, 0f, targetRotation), spinTime, RotateMode.FastBeyond360)
                                 .OnComplete(() => Debug.Log("Spin completed at multiplier: " + SpData.SpData.rewards[i].multiplier));
                    break;
                }
            }
        }
    }
}
