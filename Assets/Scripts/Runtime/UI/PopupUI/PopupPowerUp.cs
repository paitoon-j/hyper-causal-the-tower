using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupPowerUp : PopupModule
{
    [SerializeField] private Image _fill;
    private float _max = 1;

    public void Show()
    {
        base.Open();
    }

    public void Hide()
    {
        base.Close();
    }

    public void UpdateTimerBar(float duration)
    {
        this._fill.fillAmount = 0;
        this._fill.DOFillAmount(_max, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Hide();
            });
    }
}