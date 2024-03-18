using ProjectCore.Events;
using ProjectCore.Variables;
using UnityEngine;
using TMPro;

public class SpinController : SpinWheel
{
    [SerializeField] string PrefabName;
    [SerializeField] Transform RewardImageParent;
    [SerializeField] GameEvent SpinRewardEvent;
    [SerializeField] TextMeshProUGUI SpinRewardText, SpintMultiplierText;

    int NumberOfSegments;

    private void OnEnable()
    {
        SpinWheelJson spinWheelData = new SpinWheelJson();
        SpData.SpData = spinWheelData.ParseJsonFile(Resources.Load<TextAsset>("data"));
        NumberOfSegments = SpData.SpData.rewards.Length;
        GenerateRewardImages();
        SpinRewardEvent.Handler += CalculateReward;
    }


    private void GenerateRewardImages()
    {
        float degreePerSegment = 360f / NumberOfSegments;
        for (int i = 0; i < NumberOfSegments; i++)
        {
            RewardImageMaker rImg = Instantiate(Resources.Load<RewardImageMaker>(PrefabName), RewardImageParent);
            rImg.transform.RotateAround(RewardImageParent.position, Vector3.forward, degreePerSegment * i);
            rImg.SetSegments(NumberOfSegments, SpData.SpData.rewards[i].multiplier,SpData.SpData.rewards[i].color);
        }
    }

    private void CalculateReward()
    {
        Debug.Log("Here");
        int rewardAmount = SpData.SpData.coins * RewardMultiplier;
        SpintMultiplierText.text = "X" + RewardMultiplier.ToString();
        SpinRewardText.text = rewardAmount.ToString();
    }
}
