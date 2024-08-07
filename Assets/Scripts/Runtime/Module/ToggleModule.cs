using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Toggle))]
public class ToggleModule : MonoBehaviour
{
    [SerializeField] public Toggle toggle = null;

    public void SetToggleCallback(Action<bool> callback)
    {
        this.toggle.onValueChanged.RemoveAllListeners();
        this.toggle.onValueChanged.AddListener((isOn) =>
        {
            callback?.Invoke(isOn);
        });
    }
}