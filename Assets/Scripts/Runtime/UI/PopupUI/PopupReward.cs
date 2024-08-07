using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PopupReward : PopupModule
{
    [SerializeField] private GameObject _overlay;
    [SerializeField] private TextMeshProUGUI _boringSanwichPriceText;
    [SerializeField] private TextMeshProUGUI _addonPriceText;
    [SerializeField] private TextMeshProUGUI _premiumAddonPriceText;
    [SerializeField] private TextMeshProUGUI _totalPriceText;
    [SerializeField] private ButtonModule _adsRewardBtn;
    [SerializeField] private ButtonModule _backToHomeBtn;

    public void Show()
    {
        base.Open();
        SetAdsRewardCallback();
        SetBackToHomeCallback();
    }

    public void Hide()
    {
        _adsRewardBtn.SetInteractive(true);
        base.Close();
    }

    public void SetMenuPrice(string value)
    {
        _boringSanwichPriceText.text = value.ToString();
    }

    public void SetAddonPrice(string value)
    {
        _addonPriceText.text = value.ToString();
    }

    public void SetPremiumAddonPrice(string value)
    {
        _premiumAddonPriceText.text = value.ToString();
    }
    public void SetTotalPrice(string value)
    {
        _totalPriceText.text = value.ToString();
    }

    private void SetAdsRewardCallback()
    {
        _adsRewardBtn.SetCallback(() => {
            _adsRewardBtn.SetInteractive(false);
            Debug.Log("Ads Reward");
            EventEmitter.Instance.Emit(EGlobalEvent.ADS_REWARD);
        });
    }

    private void SetBackToHomeCallback()
    {
        _backToHomeBtn.SetCallback(() => {
            Debug.Log("Back to home");
            EventEmitter.Instance.Emit(EGlobalEvent.BACK_TO_HOME);
        });
    }
}
