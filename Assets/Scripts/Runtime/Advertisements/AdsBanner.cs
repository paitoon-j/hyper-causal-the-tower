using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsBanner
{
    private string _androidAdsUnitId = "Banner_Android";
    private string _iOSAdsUnitId = "Banner_iOS";
    private string _myAdId;

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

        Debug.Log("Loading Ads Banner : " + this._myAdId);
        Advertisement.Banner.Load(this._myAdId);
    }

    public void Show()
    {
        Advertisement.Banner.Show(this._myAdId);
    }

    public void Hide()
    {
        Advertisement.Banner.Hide();
    }

    public void SetPosition(BannerPosition pos)
    {
        Advertisement.Banner.SetPosition(pos);
    }
}