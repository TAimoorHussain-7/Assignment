using ProjectCore.Events;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheelView : UiPanelInAndOut
{

    [SerializeField] private Button BackButton;

    [SerializeField] private SpinWheelState SpinWheelState; 
    private void OnEnable()
    {
        BackButton.onClick.AddListener(OpenViewButtonPressed);
    }

    private void OnDisable()
    {
        BackButton.onClick.RemoveListener(OpenViewButtonPressed);
    }



    private void OpenViewButtonPressed()
    {
        /*  ShowLevelCompleteView.Invoke()*/
        SpinWheelState.GotoMainMene();
    }
}
