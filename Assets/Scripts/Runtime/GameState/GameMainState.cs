using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainState : GameState
{
    public GameMainState(GameManager manager) : base(manager)
    {
        game = manager.gameController;
        ui = manager.uiController;
        ads = manager.adsInitializer;
        data = DataManager.Instance;
        emitter = EventEmitter.Instance;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter State : " + this);
        UpdateGame();
        Init();
        ui.SetMainGameUI();
    }

    public override void OnExit()
    {
        Debug.Log("Exit State : " + this);
    }

    public override void SubscribeEvent()
    {
        ui.touchScreenArea.SetTouchScreenCallback(TouchToPlaceTower);
        ui.game.SetPauseCallback(ShowPausePopup);
        ui.game.SetSlowCallback(SlowTower);
        emitter.On(EGlobalEvent.BGM, OnBgm);
        emitter.On(EGlobalEvent.SFX, OnSfx);
        emitter.On(EGlobalEvent.BACK_TO_HOME, BackToHome);
        emitter.On(EGlobalEvent.ADS_REWARD, ShowAds);
    }

    public override void UnSubscribeEvent()
    {
        emitter.Off(EGlobalEvent.BGM, OnBgm);
        emitter.Off(EGlobalEvent.SFX, OnSfx);
        emitter.Off(EGlobalEvent.BACK_TO_HOME, BackToHome);
        emitter.Off(EGlobalEvent.ADS_REWARD, ShowAds);
    }

    ////////////////////////////////////////////////////////

    private void TouchToPlaceTower()
    {
        TouchScreenInteractive(false);
        game.SetPlaceTower(EndGame, IncreaseScore);
    }

    private void EndGame()
    {
        Debug.LogWarning("Game Over");
        SaveSettingData();
        SetRewardValue();
        ui.popup.ShowRewardPopup();
    }

    private void IncreaseScore()
    {
        TouchScreenInteractive(true);
        data.scoreData.Score++;
        UpdateScore();
    }

    ////////////////////////////////////////////////////////

    private void ShowPausePopup()
    {
        ui.popup.ShowPausePopup();
    }

    ////////////////////////////////////////////////////////

    private void UpdateGame()
    {
        ClearSettingData();
        UpdateSettingData();
        ShowHeadStartTower();
        ShowSlowTower();
        SetBaseTower();
    }

    private void SlowTower()
    {
        SlowTimeData slowData = data.powerUpData.SlowTimeData;
        game.SetSlowTower(slowData.SlowTime, slowData.Duration);
        ui.popup.ShowPowerUpPopup(slowData.Duration);
        ui.game.slowBtn.SetActive(false);
    }

    private void ShowHeadStartTower()
    {
        HeadStartData headStartData = data.powerUpData.HeadStartData;
        if (headStartData.IsHeadStart)
        {
            game.SetHeadStartTower(headStartData.StartCreateTowerConfig);
        }
        else
        {
            game.SetHeadStartTower(headStartData.StartCreateTowerDefault);
        }
    }

    private void ShowSlowTower()
    {
        SlowTimeData slowData = data.powerUpData.SlowTimeData;
        ui.game.slowBtn.SetActive(slowData.IsSlowTime);
    }

    private void SetBaseTower()
    {
        TouchScreenInteractive(false);
        bool isHeadStart = data.powerUpData.HeadStartData.IsHeadStart;
        game.SetBaseTowerAsync(isHeadStart, () =>
        {
            TouchScreenInteractive(true);
        });
    }

    ////////////////////////////////////////////////////////

    private void ShowAds()
    {
        ads.ShowAdsReward(SetRecieveAdsReward);
    }

    private void BackToHome()
    {
        game.ClearData();
        ui.home.SetActive(true);
        ui.popup.reward.Hide();
        ads.ShowAdsBanner();
        manager.ChangeState(EState.HOME);
    }

    private void SetRecieveAdsReward()
    {
        MenuResultData menuResult = data.menuResultData;
        ui.popup.SetRewardPriceValue(menuResult.MenuPrice, menuResult.AddonPrice, menuResult.PremiumAddonPrice, menuResult.Total * menuResult.Multiplier);
        SetCoin();
    }

    private void SetRewardValue()
    {
        MenuResultData menuResult = data.menuResultData;
        menuResult.MenuPrice = 1;
        menuResult.AddonPrice = data.scoreData.Score;
        menuResult.PremiumAddonPrice = 1;
        ui.popup.SetRewardPriceValue(menuResult.MenuPrice, menuResult.AddonPrice, menuResult.PremiumAddonPrice, menuResult.Total);
        SetCoin();
    }

    private void SetCoin()
    {
        MenuResultData menuResult = data.menuResultData;
        data.coinData.Coin += menuResult.Total;
        ui.game.SetCoin(data.coinData.Coin);
    }

    ////////////////////////////////////////////////////////

    private void OnBgm()
    {

    }

    private void OnSfx()
    {

    }
}