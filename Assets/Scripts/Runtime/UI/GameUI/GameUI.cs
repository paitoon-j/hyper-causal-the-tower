using UnityEngine;
using TMPro;
using System;

public class GameUI : MonoBehaviour
{
    [SerializeField] public ButtonModule touchScreen;
    [SerializeField] public ButtonModule pauseBtn;
    [SerializeField] public ButtonModule slowBtn;
    [SerializeField] private TMP_Text _towerCurrentText;
    [SerializeField] private TMP_Text _coinText;

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetInteractive(bool isInteractive)
    {
        touchScreen.SetInteractive(isInteractive);
        pauseBtn.SetInteractive(isInteractive);
        slowBtn.SetInteractive(isInteractive);
    }

    public void SetScore(int score)
    {
        _towerCurrentText.text = score.ToString();
    }

    public void SetCoin(int count)
    {
        _coinText.text = "Coin " + count.ToString();
    }

    #region callback
    public void SetTouchCallback(Action callback)
    {
        touchScreen.SetCallback(callback);
    }

    public void SetPauseCallback(Action callback)
    {
        pauseBtn.SetCallback(callback);
    }

    public void SetSlowCallback(Action callback)
    {
        slowBtn.SetCallback(callback);
    }
    #endregion
}
