using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class ButtonModule : MonoBehaviour
{
    [SerializeField] public Button button = null;

    public void SetCallback(Action onClick)
    {
        this.button.onClick.RemoveAllListeners();
        this.button.onClick.AddListener(() =>
        {
            onClick?.Invoke();
        });
    }

    public void SetInteractive(bool interactable)
    {
        this.button.interactable = interactable;
    }

    public void SetActive(bool isActive)
    {
        this.button.gameObject.SetActive(isActive);
    }
}
