using ProjectCore.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpinController : MonoBehaviour
{
    [SerializeField] string PrefabName;
    [SerializeField] Transform RewardImageParent;
    [SerializeField] int NumberOfSegments;
    [SerializeField] SpinDataSO SpData;

    [Button]
    private void OnEnable()
    {
        SpinWheelJson spinWheelData = new SpinWheelJson();
        spinWheelData.ParseJsonFile(Resources.Load<TextAsset>("data"),SpData.SpData);
    }


    private void GenerateRewardImages()
    {
        float degreePerSegment = 360f / NumberOfSegments;
        for (int i = 0; i < NumberOfSegments; i++)
        {
            RewardImageMaker rImg = Instantiate(Resources.Load<RewardImageMaker>(PrefabName), RewardImageParent);
            rImg.transform.RotateAround(RewardImageParent.position, Vector3.forward, degreePerSegment * i);
            rImg.SetSegments(NumberOfSegments);
        }
    }

}
