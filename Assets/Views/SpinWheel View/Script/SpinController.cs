using ProjectCore.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpinController : MonoBehaviour
{
    [SerializeField] string PrefabName;
    [SerializeField] Transform RewardImageParent;
    [SerializeField] SpinDataSO SpData;

    int NumberOfSegments;

    [Button]
    private void OnEnable()
    {
        SpinWheelJson spinWheelData = new SpinWheelJson();
        SpData.SpData = spinWheelData.ParseJsonFile(Resources.Load<TextAsset>("data"));
        NumberOfSegments = SpData.SpData.rewards.Length;
        GenerateRewardImages();
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

}
