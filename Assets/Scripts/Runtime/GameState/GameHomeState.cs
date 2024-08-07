using System;
using UnityEngine;

public class GameHomeState : GameState
{
    public GameHomeState(GameManager manager) : base(manager)
    {
        this.manager = manager;
        this.ui = manager.uiController;
        this.ads = manager.adsInitializer;
        this.data = DataManager.Instance;
        this.emitter = EventEmitter.Instance;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter State : " + this);
        ui.SetHomeUI();
        ads.ShowAdsBanner();
    }

    public override void OnExit()
    {
        Debug.Log("Exit State : " + this);
    }

    public override void SubscribeEvent()
    {
        ui.home.SetStartCallback(StartToSelectGame);
        emitter.On(EGlobalEvent.START_GAME, StartGame);
        emitter.On<bool>(EGlobalEvent.ON_HEAD_START, SetOnHeadStart);
        emitter.On<bool>(EGlobalEvent.ON_SLOW_TIME, SetOnSlowTime);
    }

    public override void UnSubscribeEvent()
    {
        emitter.Off(EGlobalEvent.START_GAME, StartGame);
        emitter.Off<bool>(EGlobalEvent.ON_HEAD_START, SetOnHeadStart);
        emitter.Off<bool>(EGlobalEvent.ON_SLOW_TIME, SetOnSlowTime);
    }

    private void StartGame()
    {
        ResetScore();
        ads.HideAdsBanner();
        manager.ChangeState(EState.MAIN);
    }

    private void StartToSelectGame()
    {
        ui.home.SetActive(false);
        ui.popup.ShowSelectPopup();
    }

    private void SetOnHeadStart(bool isOn)
    {
        data.powerUpData.HeadStartData.IsHeadStart = isOn;
    }

    private void SetOnSlowTime(bool isOn)
    {
        data.powerUpData.SlowTimeData.IsSlowTime = isOn;
    }
}