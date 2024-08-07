using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _iOSGameId = "5272862";
    [SerializeField] private string _androidGameId = "5272863";

    public AdsRewarded AdsRewarded { get => _adsRewarded; }
    private AdsRewarded _adsRewarded = new AdsRewarded();
    public AdsBanner AdsBanner { get => _adsBanner; }
    private AdsBanner _adsBanner = new AdsBanner();

    private bool _testMode = true;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Advertisement.Initialize(_iOSGameId, _testMode, this);
            }
            else
            {
                Advertisement.Initialize(_androidGameId, _testMode, this);
            }
        }

        SetPosAdsBanner();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        _adsBanner.Load();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    //////////////////////////////////////////////////

    public void ShowAdsReward(Action complete)
    {
        _adsRewarded.Load();
        _adsRewarded.SetOnComplete(complete);
    }

    //////////////////////////////////////////////////

    public void ShowAdsBanner()
    {
        _adsBanner.Show();
    }

    public void HideAdsBanner()
    {
        _adsBanner.Hide();
    }

    public void SetPosAdsBanner()
    {
        _adsBanner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }
}