using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase;
//using Firebase.Analytics;
using DG.Tweening;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
    void Start()
    {
        // DOVirtual.DelayedCall(.5f, () =>
        // {
        //     Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        //         var dependencyStatus = task.Result;
        //         if (dependencyStatus == Firebase.DependencyStatus.Available)
        //         {
        //             FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);

        //             var app = FirebaseApp.DefaultInstance;
        //         }
        //         else
        //         {
        //             UnityEngine.Debug.LogError(System.String.Format(
        //               "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        //         }
        //     });
        // });
    }
    // private void OnDestroy()
    // {
    //     FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd);
    // }
    // public void Win(string name_)
    // {
    //     if (Application.internetReachability == NetworkReachability.NotReachable) return;
    //     string name = name_.Replace(' ', '_');
    //     Debug.Log("Firebase Event: " + name);
    //     FirebaseAnalytics.LogEvent("Win_Picture_" + name);
    // }
    // public void WatchAdsFrame(string name_)
    // {
    //     if (Application.internetReachability == NetworkReachability.NotReachable) return;
    //     string name = name_.Replace(' ', '_');
    //     Debug.Log("Firebase Event: " + name);
    //     FirebaseAnalytics.LogEvent("Reward_Ads_Frame_" + name);
    // }
    // public void MissionFrame(string name_)
    // {
    //     if (Application.internetReachability == NetworkReachability.NotReachable) return;
    //     string name = name_.Replace(' ', '_');
    //     Debug.Log("Firebase Event: " + name);
    //     FirebaseAnalytics.LogEvent("Mission_Frame_" + name);
    // }
    // public void GetSpecialPicture(string name_)
    // {
    //     if (Application.internetReachability == NetworkReachability.NotReachable) return;
    //     string name = name_.Replace(' ', '_');
    //     Debug.Log("Firebase Event: " + name);
    //     FirebaseAnalytics.LogEvent("Open_Special_" + name);
    // }
    // public void ChooseFrame(string name_)
    // {
    //     if (Application.internetReachability == NetworkReachability.NotReachable) return;
    //     string name = name_.Replace(' ', '_');
    //     Debug.Log("Firebase Event: " + name);
    //     FirebaseAnalytics.LogEvent("Choose_Frame_" + name);
    // }
    // public void ChooseTopic(string name_)
    // {
    //     if (Application.internetReachability == NetworkReachability.NotReachable) return;
    //     string name = name_.Replace(' ', '_');
    //     Debug.Log("Firebase Event: " + name);
    //     FirebaseAnalytics.LogEvent("Choose_Topic_" + name);
    // }
    // public void ChooseLevel(string name_)
    // {
    //     if (Application.internetReachability == NetworkReachability.NotReachable) return;
    //     string name = name_.Replace(' ', '_');
    //     Debug.Log("Firebase Event: " + name);
    //     FirebaseAnalytics.LogEvent("Choose_Level_" + name);
    // }
    // public void ChooseMode(string name_)
    // {
    //     if (Application.internetReachability == NetworkReachability.NotReachable) return;
    //     string name = name_.Replace(' ', '_');
    //     Debug.Log("Firebase Event: " + name);
    //     FirebaseAnalytics.LogEvent("Choose_Mode_" + name);
    // }
    // public void CompletePicture(string name_)
    // {
    //     if (Application.internetReachability == NetworkReachability.NotReachable) return;
    //     string name = name_.Replace(' ', '_');
    //     Debug.Log("Firebase Event: " + name);
    //     FirebaseAnalytics.LogEvent("Complete_picture_" + name);
    // }
    // public void ShowInter(string name_)
    // {
    //     if (Application.internetReachability == NetworkReachability.NotReachable) return;
    //     string name = name_.Replace(' ', '_');
    //     Debug.Log("Firebase Event: " + name);
    //     FirebaseAnalytics.LogEvent("Show_Inter_" + name);
    // }

}
