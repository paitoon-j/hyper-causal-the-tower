using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class HomeUI : MonoBehaviour
{
    [SerializeField] public ButtonModule startButton;

    public void SetActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }

    public void SetInteractive(bool isInteractive)
    {
        this.startButton.SetInteractive(isInteractive);
    }

    #region callback
    public void SetStartCallback(Action callback)
    {
        this.startButton.SetCallback(callback);
    }
    #endregion
}
