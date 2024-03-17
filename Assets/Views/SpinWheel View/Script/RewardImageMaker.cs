using MPUIKIT;
using UnityEngine;

public class RewardImageMaker : MonoBehaviour
{
    [SerializeField] MPImageBasic ClockImage,  AntiClockImage;
    public void SetSegments(int numberOfSegments)
    {
        if (numberOfSegments > 0)
        {
            float imageFillAmount = 1f/numberOfSegments;
            SetFillAmount(imageFillAmount);
        }
    }
    private void SetFillAmount(float fillPercentage)
    {
        float fillAmountPerSide = fillPercentage / 2f;
        ClockImage.fillAmount = fillAmountPerSide;
        AntiClockImage.fillAmount = fillAmountPerSide;
    }
}
