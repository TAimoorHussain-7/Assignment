using ProjectCore.Events;
using ProjectCore.Variables;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class SpinController : SpinWheel
{
    [SerializeField] string PrefabName;
    [SerializeField] Transform RewardImageParent;
    [SerializeField] GameEvent SpinRewardEvent, ChangePositionEvent, ShuffleEvent;
    [SerializeField] TextMeshProUGUI SpinRewardText, SpintMultiplierText;
    [SerializeField] float ShuffleDelay=0.01f;
    [SerializeField] SpinDataSO SpData;

    int NumberOfSegments;
    Coroutine ShuffleRotine;
    List<RewardImageMaker> RimgTransform;

    private void OnEnable()
    {
        SpinWheelJson spinWheelData = new SpinWheelJson();
        SpData.SpData = spinWheelData.ParseJsonFile(Resources.Load<TextAsset>("data"));
        NumberOfSegments = SpData.SpData.rewards.Length;
        GenerateRewardImages();
        SpinRewardEvent.Handler += CalculateReward;
        ShuffleEvent.Handler += ShuffleIndex;
    }

    private void OnDisable()
    {
        SpinRewardEvent.Handler -= CalculateReward;
        ShuffleEvent.Handler -= ShuffleIndex;
    }

    private void GenerateRewardImages()
    {
        RimgTransform = new List<RewardImageMaker>();
        degreePerSegment = 360f / NumberOfSegments;
        for (int i = 0; i < NumberOfSegments; i++)
        {
            RewardImageMaker rImg = Instantiate(Resources.Load<RewardImageMaker>(PrefabName), RewardImageParent);
            rImg.transform.localRotation = Quaternion.Euler(0, 0, i * degreePerSegment);
            rImg.SetSegments(NumberOfSegments, SpData.SpData.rewards[i].multiplier,SpData.SpData.rewards[i].color);
            RimgTransform.Add(rImg);
        }
        Invoke(nameof(CallChangePosition),2);
    }

    private void CallChangePosition()
    {
        ChangePositionEvent.Invoke();
    }
    private void ShuffleIndex()
    {
        for (int i = SpData.SpData.rewards.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = SpData.SpData.rewards[i];
            SpData.SpData.rewards[i] = SpData.SpData.rewards[j];
            SpData.SpData.rewards[j] = temp;
        }
        ShuffleRotine = StartCoroutine(ShuffleWheel());
    }
    private IEnumerator ShuffleWheel()
    {
        for (int i = 0; i < NumberOfSegments; i++)
        {
            RimgTransform[i].SetSegments(NumberOfSegments, SpData.SpData.rewards[i].multiplier, SpData.SpData.rewards[i].color);
            yield return new WaitForSeconds(ShuffleDelay);
        }

        if (ShuffleRotine != null)
        {
            StopCoroutine(ShuffleRotine);
        }
    }

    private void CalculateReward()
    {
        int rewardAmount = SpData.SpData.coins * RewardMultiplier;
        SpintMultiplierText.text = "X" + RewardMultiplier.ToString();
        SpinRewardText.text = rewardAmount.ToString();
        ChangePositionEvent.Invoke();
    }
}
