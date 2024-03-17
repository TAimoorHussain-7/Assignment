using ProjectCore.Events;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : UiPanelInAndOut
{
    [SerializeField] private Button PlayButton, SpinWheelButton;

    [SerializeField] private GameEvent ShowLevelCompleteView;

    [SerializeField] private MainMenuState MainMenuState;

    private void OnEnable()
    {
        PlayButton.onClick.AddListener(OpenViewButtonPressed);
        SpinWheelButton.onClick.AddListener(OpenSpinWheelButtonPressed);
    }

    private void OnDisable()
    {
        PlayButton.onClick.RemoveListener(OpenViewButtonPressed);
        SpinWheelButton.onClick.RemoveListener(OpenSpinWheelButtonPressed);
    }

    private void OpenViewButtonPressed()
    {
        /*  ShowLevelCompleteView.Invoke()*/
        MainMenuState.GotoGame();
    }
    private void OpenSpinWheelButtonPressed()
    {
        /*  ShowLevelCompleteView.Invoke()*/
        MainMenuState.GotoSpinWheel();
    }
}
