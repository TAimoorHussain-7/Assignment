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
    [SerializeField] Button SpinButton;
    [SerializeField] Transform wheelTransform;
    [SerializeField] Transform wheelParent;
    [SerializeField] GameObject RewardPanel;
    [SerializeField] GameEvent SpinRewardEvent, ChangePositionEvent, ShuffleEvent;
    [SerializeField] float AnimationDuration = 1;
    [SerializeField] SpinDataSO SpData;
    [SerializeField] SpinAnimtionSo SpinAnimation;

    private Tween wheelTween;
    private Coroutine ResetRotine, RotateRotine;

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
            int randomInd = SelectRandomIndex();
            float targetAngle = 360 - (degreePerSegment * randomInd); 
            float spinTime = Random.Range(SpinAnimation.minSpinTime, SpinAnimation.maxSpinTime);
            int currentSpeed = (int)Mathf.Round(SpinAnimation.SpinSpeed);
            targetAngle += (360*currentSpeed);
            RotateRotine = StartCoroutine(RotateSpinNow(targetAngle, spinTime, randomInd));
        }
    }

    private IEnumerator RotateSpinNow(float targetAngle,float rotationTime, int randomInd)
    {
        float totalRotation = SpinAnimation.clockwise ? 0 - targetAngle : targetAngle - 0;
        float step = totalRotation / rotationTime;

        float elapsedTime = 0f;

        while (elapsedTime < rotationTime)
        {
            wheelTransform.Rotate(Vector3.forward, step * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        wheelTransform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
        RewardMultiplier = SpData.SpData.rewards[randomInd].multiplier;
        SpinRewardEvent.Invoke();

        if (RotateRotine != null)
        {
            StopCoroutine(RotateRotine);
        }
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
