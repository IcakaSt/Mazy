using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Mediation;

/*/
public class UnityAdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // Initialize package with a custom Game ID
    InitializationOptions options = new InitializationOptions();
    options.SetGameId("TestGameId");
await UnityServices.InitializeAsync(options);


    [SerializeField] string _androidAdUnitId = "4623843";
    [SerializeField] string _iOsAdUnitId = "4623842";
    [SerializeField] string GAME_ID = "3003911"; //replace with your gameID from dashboard. note: will be different for each platform.

    private const string VIDEO_PLACEMENT = "video";
    private const string REWARDED_VIDEO_PLACEMENT = "rewardedVideo";

   bool testMode = false;

    //utility wrappers for debuglog
    public delegate void DebugEvent(string msg);
    public static event DebugEvent OnDebugLog;

    public int rewardCase = 0;
    private void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        GAME_ID = (Application.platform == RuntimePlatform.IPhonePlayer)
       ? _iOsAdUnitId
       : _androidAdUnitId;
        Initialize();
    }
    public void Initialize()
    {
        if (Advertisement.isSupported)
        {
            DebugLog(Application.platform + " supported by Advertisement");
        }
        Advertisement.Initialize(GAME_ID, testMode, this);
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(REWARDED_VIDEO_PLACEMENT, this);
        ShowRewardedAd();
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(REWARDED_VIDEO_PLACEMENT, this);
    }

    public void LoadNonRewardedAd()
    {
        if (PlayerPrefs.GetInt("RewardedAdd") == 0)
        {
            Advertisement.Load(VIDEO_PLACEMENT, this);
            ShowNonRewardedAd();
            PlayerPrefs.SetInt("RewardedAdd", 1);
        }
        if (PlayerPrefs.GetInt("RewardedAdd") == 1)
        {
            PlayerPrefs.SetInt("RewardedAdd", 0);
        }

    }

    public void ShowNonRewardedAd()
    {
        Advertisement.Show(VIDEO_PLACEMENT, this);
    }

    public void OnInitializationComplete()
    {
        DebugLog("Init Success");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        DebugLog($"Init Failed: [{error}]: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        DebugLog($"Load Success: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        DebugLog($"Load Failed: [{error}:{placementId}] {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        DebugLog($"OnUnityAdsShowFailure: [{error}]: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        DebugLog($"OnUnityAdsShowStart: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        DebugLog($"OnUnityAdsShowClick: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        DebugLog($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");

        switch (rewardCase)
        {
            case 1: PlayerPrefs.SetFloat("MoneyToAdd", 15000); Debug.Log("changed"); break;
            case 2: PlayerPrefs.SetFloat("timeMazeRuner",60); PlayerPrefs.SetInt("Restarted", 1); SceneManager.LoadScene("Maze Runer"); break;
        }
    }

    //wrapper around debug.log to allow broadcasting log strings to the UI
    void DebugLog(string msg)
    {
        OnDebugLog?.Invoke(msg);
        Debug.Log(msg);
    }
}

/*/