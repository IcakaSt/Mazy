using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityAdsExampleUI : MonoBehaviour
{
    /*/
        public UnityAdsManager unityAdsManager;
        public Button[] loadRewardedBtn;
        public Button[] loadInterstitialBtn;

        public Text debugLogText;


        private string textLog = "DEBUG LOG: \n";

        private void Awake()
        {
            //if you didn't assign in the inspector
            if (unityAdsManager == null)
            {
                unityAdsManager = FindObjectOfType<UnityAdsManager>();
                unityAdsManager.Initialize();
            }
        }

        private void Start()
        {
            if (loadRewardedBtn != null)
            {
                foreach (Button but in loadRewardedBtn)
                {
                    but.onClick.AddListener(unityAdsManager.LoadRewardedAd);
                }
            }

            if (loadInterstitialBtn != null)
            {
                foreach (Button but in loadInterstitialBtn)
                {
                    but.onClick.AddListener(unityAdsManager.LoadNonRewardedAd);
                }
            }
        }
    /*/
}
