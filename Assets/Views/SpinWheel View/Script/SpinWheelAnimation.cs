using DG.Tweening;
using ProjectCore.Events;
using ProjectCore.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpinWheelAnimation : SpinWheel
{
    [SerializeField] RotateMode rm;
    [SerializeField] Button SpinButton;
    [SerializeField] Transform wheelTransform;
    [SerializeField] Transform wheelParent;
    [SerializeField] GameObject RewardPanel;
    [SerializeField] GameEvent SpinRewardEvent, ChangePositionEvent, ShuffleEvent;
    [SerializeField] float AnimationDuration = 1;
    [SerializeField] SpinDataSO SpData;
    [SerializeField] SpinAnimtionSo SpinAnimation;

    private Tween wheelTween;
    private Coroutine ResetRotine;
    int RandomInd = -1;

    private void OnEnable()
    {
        SpinButton.onClick.AddListener(OnSpinWheel);
        ChangePositionEvent.Handler += ChangePosition;
    }

    private void OnDisable()
    {
        SpinButton.onClick.RemoveListener(OnSpinWheel);
        ChangePositionEvent.Handler -= ChangePosition;
    }

    [Button]
    private void OnSpinWheel()
    {
        if (!IsSpining)
        {
            IsSpining = true;
            SpinButton.interactable = false;
            RandomInd = SelectRandomIndex();
            float targetAngle = 360 - (degreePerSegment * RandomInd); 
            int currentSpeed = (int)Mathf.Round(SpinAnimation.SpinSpeed);
            targetAngle = SpinAnimation.clockwise ? targetAngle -(360 * currentSpeed) : targetAngle + (360 * currentSpeed);
            switch (SpinAnimation.CurrentRotateType)
            {
                case RotateType.Normal:
                    float spinTime = Random.Range(SpinAnimation.minSpinTime, SpinAnimation.maxSpinTime);
                    NormalSpin(targetAngle,spinTime);
                    break;

                case RotateType.StartFast:
                    StartFast(targetAngle);
                    break;

                case RotateType.StartSlow:
                    StartSlowly(targetAngle);
                    break;
            }
        }
    }

    private void NormalSpin(float targetAngle, float spinTime)
    {
        wheelTransform.DORotate(new Vector3(0, 0, targetAngle), spinTime, RotateMode.FastBeyond360).SetEase(SpinAnimation.SpinEase).OnComplete(() => {
            Invoke(nameof(SendRewardValue),1);
        });
    }

    private void StartFast(float targetAngle)
    {
        float segmentAngle = targetAngle / 4f;
        wheelTransform.DORotate(new Vector3(0, 0, segmentAngle * 3), SpinAnimation.fastDuration, RotateMode.FastBeyond360)
            .SetEase(SpinAnimation.SpinEase)
            .OnComplete(() => {
                wheelTransform.DORotate(new Vector3(0, 0, targetAngle), SpinAnimation.fastDuration/2)
                    .SetEase(SpinAnimation.SpinEase)
                    .OnUpdate(() => {
                        float currentSpeed = Mathf.Lerp(SpinAnimation.fastDuration, SpinAnimation.fastDuration / 2, 0.01f);
                        wheelTransform.Rotate(Vector3.forward, segmentAngle * Time.deltaTime / currentSpeed);
                    })
                    .OnComplete(() => {
                        Invoke(nameof(SendRewardValue), 1);
                    });
            });
    }

    private void StartSlowly(float targetAngle)
    {
        float segmentAngle = targetAngle / 4f;
        Sequence rotationSequence = DOTween.Sequence();
        rotationSequence.Append(wheelTransform.DORotate(new Vector3(0, 0, segmentAngle), SpinAnimation.fastDuration / 2)
            .SetEase(Ease.OutQuad)
            .OnUpdate(() => {
                // Calculate the current angle based on the progress
                float currentAngle = Mathf.Lerp(0, segmentAngle, rotationSequence.ElapsedPercentage());
                wheelTransform.rotation = Quaternion.Euler(0, 0, currentAngle);
            })
           .OnComplete(() =>
             {
                 wheelTransform.DORotate(new Vector3(0, 0, segmentAngle * 3), SpinAnimation.fastDuration, RotateMode.FastBeyond360)
            .SetEase(SpinAnimation.SpinEase)
            .OnComplete(() =>
            {
                wheelTransform.DORotate(new Vector3(0, 0, targetAngle), SpinAnimation.fastDuration / 2)
                    .SetEase(Ease.OutQuad)
                    .OnUpdate(() =>
                    {
                        float currentAngle = Mathf.Lerp(0, segmentAngle, rotationSequence.ElapsedPercentage());
                        wheelTransform.rotation = Quaternion.Euler(0, 0, currentAngle);
                    })
                    .OnComplete(() =>
                    {
                        Invoke(nameof(SendRewardValue), 1);
                    });
            });

             }));
    }

    private void SendRewardValue()
    {
        RewardMultiplier = SpData.SpData.rewards[RandomInd].multiplier;
        SpinRewardEvent.Invoke();
    }

    public int SelectRandomIndex()
    {
        List<float> probabilities = new List<float>();

        new List<float>();
        foreach (RewardData rD in SpData.SpData.rewards)
        {
            probabilities.Add(rD.probability);
        }

        List<float> normalizedProbabilities = NormalizeProbabilities(probabilities);

        float randomValue = Random.value;
        float cumProbability = 0f;

        for (int i = 0; i < normalizedProbabilities.Count; i++)
        {
            cumProbability += normalizedProbabilities[i];
            if (randomValue <= cumProbability)
            {
                return i;
            }
        }

        return -1;
    }

    private List<float> NormalizeProbabilities(List<float> probabilities)
    {
        float sum = 0f;
        foreach (float probability in probabilities)
        {
            sum += probability;
        }

        List<float> normalizedProbabilities = new List<float>();
        foreach (float probability in probabilities)
        {
            normalizedProbabilities.Add(probability / sum);
        }

        return normalizedProbabilities;
    }

    private void ChangePosition()
    {
        if (!IsSpining)
        {
            RewardPanel.transform.DOLocalMoveY(-320, 0.01f).OnComplete(() =>
            {
                RewardPanel.SetActive(false);
            });
            wheelParent.DOScale(Vector3.one, AnimationDuration);

            wheelTween = wheelParent.DOLocalMoveY(165, AnimationDuration).OnComplete(() =>
            {
                SpinButton.interactable = true;
            });
            ShuffleEvent.Invoke();
            wheelTransform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {
            wheelParent.DOScale(Vector3.one * 0.7f, AnimationDuration);
            wheelTween = wheelParent.DOLocalMoveY(650, AnimationDuration).OnUpdate(() =>
            {
                float progress = wheelTween.ElapsedPercentage();
                if (progress > 0.9f)
                {
                    ResetRotine = StartCoroutine(ResetWheel());
                }
            });
            RewardPanel.SetActive(true);
            RewardPanel.transform.DOLocalMoveY(-125, AnimationDuration);
        }
    }

    private IEnumerator ResetWheel()
    {
        yield return new WaitForSeconds(5);
        IsSpining = false;
        ChangePosition();
        if (ResetRotine != null)
        {
            StopCoroutine(ResetRotine);
        }
    }
}
