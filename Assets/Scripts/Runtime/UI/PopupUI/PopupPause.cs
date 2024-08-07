using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPause : PopupModule
{
    [SerializeField] private GameObject _overlay;
    [SerializeField] private ButtonModule _closeBtn;
    [SerializeField] private ButtonModule _bgmBtn;
    [SerializeField] private ButtonModule _sfxBtn;
    [SerializeField] private ButtonModule _backToHomeBtn;

    public void Show()
    {
        Debug.Log("Show popup pause");
        base.Open();
        SetCloseCallback();
        SetBgmCallback();
        SetSfxCallback();
        SetBackToHomeCallback();
    }

    public void Hide()
    {
        base.Close();
    }

    private void SetCloseCallback()
    {
        _closeBtn.SetCallback(() => {
            Debug.Log("Close popup pause");
            Hide();
        });
    }

    private void SetBgmCallback()
    {
        _bgmBtn.SetCallback(() => {
            Debug.Log("BGM");
            EventEmitter.Instance.Emit(EGlobalEvent.BGM);
        });
    }

    private void SetSfxCallback()
    {
        _sfxBtn.SetCallback(() => {
            Debug.Log("SFX");
            EventEmitter.Instance.Emit(EGlobalEvent.SFX);
        });
    }

    private void SetBackToHomeCallback()
    {
        _backToHomeBtn.SetCallback(() => {
            Debug.Log("Back to home");
            EventEmitter.Instance.Emit(EGlobalEvent.BACK_TO_HOME);
            Hide();
        });
    }
}
