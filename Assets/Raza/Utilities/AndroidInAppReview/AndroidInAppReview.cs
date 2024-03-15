using UnityEngine;
using System.Collections;
using ProjectCore.Variables;
#if UNITY_EDITOR || UNITY_ANDROID
// using Google.Play.Review;
#endif


namespace CustomUtilities
{
    [CreateAssetMenu(fileName = "AndroidInAppReview", menuName = "ProjectCore/Android/AndroidInAppReview")]
    public class AndroidInAppReview : ScriptableObject
    {
//         [Header("Game Level")] 
//         [SerializeField] private Int GameLevel;
//         
//
//         #region Public Functions
//
//         public void LoadRequestInfo(MonoBehaviour monoBehaviour)
//         {
// #if UNITY_EDITOR || UNITY_ANDROID
//             //if(!CanShowRating()) return;
//             
//             _reviewManager = new ReviewManager();
//             monoBehaviour.StartCoroutine(RequestInfoCoroutine());
//             #endif
//             
//         }
//         
//         public void TriggerInAppRatingFlow(MonoBehaviour monoBehaviour)
//         {
// #if UNITY_EDITOR || UNITY_ANDROID
//             Debug.Log($"Requesting Rating :: ReviewManager {_reviewManager == null} || PlayReview :: {_playReviewInfo == null}");
//             if (_reviewManager == null || _playReviewInfo == null)
//             {
//                 Debug.Log($"Requesting Rating :: ReviewManager || PlayReview :: NULL");
//                 // Application.OpenURL("https://play.google.com/store/apps/details?id=gameID");
//                 return;
//             }
//
//             monoBehaviour.StartCoroutine(InAppReviewCoroutine());
// #endif
//             
//         }
//
//         #endregion
//         
//
//         #region Private Functions
//         
// #if UNITY_EDITOR || UNITY_ANDROID
//
//         private ReviewManager _reviewManager =null;
//         private PlayReviewInfo _playReviewInfo = null;
//         private bool CanShowRating() => GameLevel == 5;
//
//         private IEnumerator RequestInfoCoroutine()
//         {
//             var requestFlowOperation = _reviewManager.RequestReviewFlow();
//             yield return requestFlowOperation;
//             if (requestFlowOperation.Error != ReviewErrorCode.NoError)
//             {
//                 Debug.Log($"****************GoogleInApp Review : RequestingInAppReviewFlow() : SUCCESSS");
//                 yield break;
//             }
//             else
//             {
//                 Debug.Log($"****************GoogleInApp Review : RequestingInAppReviewFlow() : {requestFlowOperation.Error}");
//             }
//             _playReviewInfo = requestFlowOperation.GetResult();
//         }
//
//         private IEnumerator InAppReviewCoroutine()
//         {
//             var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
//             yield return launchFlowOperation;
//             _playReviewInfo = null; // Reset the object
//             if (launchFlowOperation.Error != ReviewErrorCode.NoError)
//             {
//                 Debug.Log($"****************GoogleInApp Review : LaunchingInAppReviewFlow() : {launchFlowOperation.Error}");
//                 yield break;
//             }
//             else
//             {
//                 Debug.Log($"****************GoogleInApp Review : LaunchingInAppReviewFlow() : {launchFlowOperation.Error}");
//                 UnityEngine.Application.OpenURL("https://play.google.com/store/apps/details?id=gameID");
//             }
//         }
//
//
//         
// #endif
//         #endregion
        
        
    }
}
