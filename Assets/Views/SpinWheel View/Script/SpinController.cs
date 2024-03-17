using UnityEngine;

public class SpinController : MonoBehaviour
{
    [SerializeField] string PrefabName;
    [SerializeField] Transform RewardImageParent;

    private void OnEnable()
    {
        RewardImageMaker rImg = Instantiate(Resources.Load<RewardImageMaker>(PrefabName), RewardImageParent);
        rImg.SetSegments(8);
    }

}
