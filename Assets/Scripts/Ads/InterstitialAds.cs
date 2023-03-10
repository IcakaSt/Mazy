using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;

public class InterstitialAds : MonoBehaviour
{
    IInterstitialAd ad;
    string adUnitId = "Interstitial_Android";
    string gameId = "4623842";
    [SerializeField] string _androidAdUnitId = "4623843";
    [SerializeField] string _iOsAdUnitId = "4623842";

    private void Start()
    {
        InitServices();
    }
    public async void InitServices()
    {
        gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
    ? _iOsAdUnitId
    : _androidAdUnitId;

        try
        {
            InitializationOptions initializationOptions = new InitializationOptions();
            initializationOptions.SetGameId(gameId);
            await UnityServices.InitializeAsync(initializationOptions);

            InitializationComplete();
        }
        catch (Exception e)
        {
            InitializationFailed(e);
        }
    }

    public void SetupAd()
    {
        //Create
        ad = MediationService.Instance.CreateInterstitialAd(adUnitId);

        //Subscribe to events
        ad.OnLoaded += AdLoaded;
        ad.OnFailedLoad += AdFailedLoad;

        ad.OnShowed += AdShown;
        ad.OnFailedShow += AdFailedShow;
        ad.OnClosed += AdClosed;
        ad.OnClicked += AdClicked;

        // Impression Event
        MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
    }

    public void ShowAd()
    {
        Debug.Log("Show add " + PlayerPrefs.GetInt("AdShown"));
        if (ad.AdState == AdState.Loaded && PlayerPrefs.GetInt("AdShown") == 1)
        {
            PlayerPrefs.SetInt("AdShown", 0);
            ad.Show();
            PlayerPrefs.SetInt("NonRewarded", 1);
        }
        else
        { PlayerPrefs.SetInt("AdShown", 1); }
    }

    void InitializationComplete()
    {
        SetupAd();
        ad.Load();
    }

    void InitializationFailed(Exception e)
    {
        Debug.Log("Initialization Failed: " + e.Message);
    }

    void AdLoaded(object sender, EventArgs args)
    {
      //  Debug.Log("Ad loaded");
    }

    void AdFailedLoad(object sender, LoadErrorEventArgs args)
    {
        Debug.Log("Failed to load ad");
        Debug.Log(args.Message);
    }

    void AdShown(object sender, EventArgs args)
    {
//Debug.Log("Ad shown!");
    }

    void AdClosed(object sender, EventArgs e)
    {
        // Pre-load the next ad
        ad.Load();
      //  Debug.Log("Ad has closed");

        // Execute logic after an ad has been closed.
    }

    void AdClicked(object sender, EventArgs e)
    {
        Debug.Log("Ad has been clicked");
        // Execute logic after an ad has been clicked.
    }

    void AdFailedShow(object sender, ShowErrorEventArgs args)
    {
        Debug.Log(args.Message);
    }

    void ImpressionEvent(object sender, ImpressionEventArgs args)
    {
        var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
       // Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
    }

}

