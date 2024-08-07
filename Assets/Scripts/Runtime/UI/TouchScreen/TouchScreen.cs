using System;
using UnityEngine;

public class TouchScreen : MonoBehaviour
{
    [SerializeField] private ButtonModule _touchScreenArea;

    public void SetActive(bool isActive)
    {
        this._touchScreenArea.SetActive(isActive);
    }

    public void SetInteractive(bool isInteractive)
    {
        this._touchScreenArea.SetInteractive(isInteractive);
    }

    public void SetTouchScreenCallback(Action callback)
    {
        this._touchScreenArea.SetCallback(callback);
    }
}
