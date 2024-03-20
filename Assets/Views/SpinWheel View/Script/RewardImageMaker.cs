using MPUIKIT;
using UnityEngine;
using TMPro;

public class RewardImageMaker : MonoBehaviour
{
    [SerializeField] MPImageBasic ClockImage,  AntiClockImage;
    [SerializeField] TextMeshProUGUI MultiplierText;

    public void SetSegments(int numberOfSegments, int multiplier, string colour)
    {
        if (numberOfSegments > 0)
        {
            float imageFillAmount = 1f/numberOfSegments;
            MultiplierText.text = "X" + multiplier.ToString();
            Color newColor;
            if (ColorUtility.TryParseHtmlString(colour, out newColor))
            {
                // Assign color to image
                ClockImage.color = newColor;
                AntiClockImage.color = newColor;
            }
            else
            {
                Debug.LogError("Failed to parse color from hexadecimal string: " + colour);
            }
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
