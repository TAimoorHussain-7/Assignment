using System;
using DG.Tweening;
using ProjectCore.Events;
using ProjectCore.StateMachine;
using ProjectCore.Application;
using ProjectCore.GamePlay;
using ProjectCore.UI;
using UnityEngine;


namespace CustomUtilities
{
    public class AndroidBackButton : MonoBehaviour
    {
        [Header("State Machine")]
        [SerializeField] private FiniteStateMachine StateMachine;

        [SerializeField] private GameEvent ShowDailyChallengeQuitView;
        [SerializeField] private GameEventWithBool ShowSettingView;

        private bool _canShowQuitView = true;
        private float _lastTimeBackButtonPressed = 0;
        private float _deltaTime = 1.5f;


#if UNITY_ANDROID || UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape) && Time.time >= _lastTimeBackButtonPressed)
            {
                _lastTimeBackButtonPressed = Time.time + _deltaTime;
                try
                {
                    BackButtonPressed();
                }
                catch (Exception e)
                {
                    Debug.Log($"Back Button Pressed : {e}");
                }
                
            }
        }
#endif

        private void BackButtonPressed()
        {
            var currentState = StateMachine.RunningState;

            // if (currentState is MainMenuState)
            // {
            //     MainMenuState state = currentState as MainMenuState;
            //     state.ShowQuitView();
            // }
            // else if (currentState is GameState)
            // {
            //     //showquite or show settings 
            // }
            // else if (currentState is UIViewState)
            // {
            //     UIViewState state = currentState as UIViewState;
            //     state.CloseView();
            //     
            // }
            //

            if (/*currentState as MainMenuViewState*/ currentState!=null )
            {
                /*var uiState = currentState as MainMenuViewState;
                var mainMenuView = uiState.GetMainMenuView();

                uiState.ShowQuitGameView.Invoke();*/

            }
            // else if(currentState as NormalGameState)
            // {
            //   ShowSettingView.Raise(false);   
            // }
            // else if(currentState as StacksChallengeGameState)
            // {
            //     ShowDailyChallengeQuitView.Invoke();
            // }
            // else if(currentState as HiddenTilesChallengeGameState)
            // {
            //     ShowDailyChallengeQuitView.Invoke();
            // }
            // else if (currentState as TilesCollectChallengeGameState)
            // {
            //     ShowDailyChallengeQuitView.Invoke();
            // }
            // else if(currentState as SettingsState)
            // {
            //     var settingsState = currentState as SettingsState;
            //     var isMainMenu = settingsState.HideAdvanceOptions;
            //     var closeReason = UICloseReasons.ResumeGame;
            //     settingsState.CloseView(closeReason);
            // }
            // else if (currentState as LevelFailState)
            // {
            //     var uiState = currentState as LevelFailState;
            //     uiState.CloseView(UICloseReasons.Game);
            // }


            else if (currentState as QuitLevelState)
            {
                var uiState = currentState as QuitLevelState;
                uiState.CloseView(UICloseReasons.ResumeGame);
            }
           
            else if(currentState as UIViewState)
            {
                var uiViewState = currentState as UIViewState;
                uiViewState.CloseView();
            }
        }

    }
}
