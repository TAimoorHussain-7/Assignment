using Sirenix.OdinInspector;
using UnityEngine;

public class SpinController : MonoBehaviour
{
    [SerializeField] string PrefabName;
    [SerializeField] Transform RewardImageParent;
    [SerializeField] int NumberOfSegments;

    [Button]
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
