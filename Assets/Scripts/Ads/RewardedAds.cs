using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RewardedAds : MonoBehaviour
{
        IRewardedAd ad;
        string adUnitId = "Rewarded_Android";
        string gameId = "4623843";
        [SerializeField] string _androidAdUnitId = "4623843";
        [SerializeField] string _iOsAdUnitId = "4623842";

        public int rewardCase = 0;

    private void Start()
    {
        InitServices();
    }

    private void Update()
    {
        if (ad.AdState == AdState.Loaded)
        {
            PlayerPrefs.SetInt("RewardedLoaded", 1);
        }
        else { PlayerPrefs.SetInt("RewardedLoaded", 0); }
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
            ad = MediationService.Instance.CreateRewardedAd(adUnitId);

            //Subscribe to events
            ad.OnLoaded += AdLoaded;
            ad.OnFailedLoad += AdFailedLoad;

            ad.OnShowed += AdShown;
            ad.OnFailedShow += AdFailedShow;
            ad.OnClosed += AdClosed;
            ad.OnClicked += AdClicked;
            ad.OnUserRewarded += UserRewarded;

            // Impression Event
            MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
        }

        public void ShowAd()
        {
            if (ad.AdState == AdState.Loaded)
            {
                PlayerPrefs.SetInt("NonRewarded", 0);
                ad.Show();
            }
        }

        void InitializationComplete()
        {
            SetupAd();
            ad.Load();
        }

        void InitializationFailed(Exception e)
        {
      //Debug.Log("Initialization Failed: " + e.Message);
        }

        void AdLoaded(object sender, EventArgs args)
        {
      //Debug.Log("Ad loaded");
        }

        void AdFailedLoad(object sender, LoadErrorEventArgs args)
        {
            Debug.Log("Failed to load ad");
            Debug.Log(args.Message);
        }

        void AdShown(object sender, EventArgs args)
        {
    //        Debug.Log("Ad shown!");
        }

        void AdClosed(object sender, EventArgs e)
        {
            // Pre-load the next ad
            ad.Load();
      //      Debug.Log("Ad has closed");
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
            Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);

            if (PlayerPrefs.GetInt("NonRewarded")==0)
            {
                switch (rewardCase)
                {
                    case 1:
                    PlayerPrefs.SetFloat("MoneyToAdd", 15000); Debug.Log("changed");
                    break;

                    case 2:
                    if (PlayerPrefs.GetInt("Countinue") == 0)
                    {
                        PlayerPrefs.SetFloat("timeMazeRuner", 60); PlayerPrefs.SetInt("Restarted", 1); SceneManager.LoadScene("Maze Runer"); PlayerPrefs.SetInt("Countinue", 1);
                    }
                    break;
                }
            }
        }

        void UserRewarded(object sender, RewardEventArgs e)
        {
            Debug.Log($"Received reward: type:{e.Type}; amount:{e.Amount}");

          
        }

    }
