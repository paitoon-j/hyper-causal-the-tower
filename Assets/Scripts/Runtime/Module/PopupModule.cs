using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupModule : MonoBehaviour
{
    private string _id;
    private Action _onOpen;
    private Action _onClose;
    private Image _popup;

    public bool isOpen
    {
        get { return this.gameObject.activeSelf; }
    }

    public string Open(bool isAnimate = false)
    {
        this._popup = this.gameObject.GetComponent<Image>();

        if (isAnimate)
        {
            this.gameObject.SetActive(true);
            this.FadeOpen();
        }
        else
        {
            this.gameObject.SetActive(true);
        }

        this._onOpen?.Invoke();
        return this._id;
    }

    public string Close(bool isAnimate = false)
    {
        if (isAnimate)
        {
            this.FadeClose(() => {
                this.gameObject.SetActive(false);
            });
        }
        else
        {
            this.gameObject.SetActive(false);
        }

        this._onClose?.Invoke();
        return this._id;
    }

    private void FadeOpen()
    {
        this._popup.DOFade(1f, 0.2f).SetEase(Ease.Linear);
    }

    private void FadeClose(Action callback)
    {
        this._popup.DOFade(0f, 0.1f)
            .SetEase(Ease.Linear)
            .OnComplete(()=> {
                callback?.Invoke();
            });
    }

    public void OnOpen(Action callback)
    {
        this._onOpen = callback;
    }

    public void OnClose(Action callback)
    {
        this._onClose = callback;
    }
}
