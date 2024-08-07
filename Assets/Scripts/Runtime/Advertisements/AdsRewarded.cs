using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;

public class AdsRewarded : IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string _androidAdsUnitId = "Rewarded_Android";
    private string _iOSAdsUnitId = "Rewarded_iOS";
    private string _myAdId;
    private Action _rewardComplete;

    public void Load()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            this._myAdId = this._iOSAdsUnitId;
        }
        else
        {
            this._myAdId = this._androidAdsUnitId;
        }

        Advertisement.Load(this._myAdId, this);
        Debug.Log("Loading Ads Reward : " + this._myAdId);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Loaded complete : " + adUnitId);
        if (adUnitId.Equals(this._myAdId))
        {
            Advertisement.Show(this._myAdId, this);
        }
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(this._myAdId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            this._rewardComplete?.Invoke();
            Debug.Log("Unity Ads Rewarded Ad Completed");
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void SetOnComplete(Action complete)
    {
        this._rewardComplete = complete;
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}