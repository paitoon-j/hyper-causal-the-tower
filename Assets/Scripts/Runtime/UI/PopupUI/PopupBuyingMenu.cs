using UnityEngine;
using System;
using TMPro;

public class PopupBuyingMenu : PopupModule
{
    [SerializeField] private TextMeshProUGUI _message;
    [SerializeField] private ButtonModule _closeBtn;
    [SerializeField] private ButtonModule _buyBtn;

    private const string _frontMessage  = "Do you want to unlock";
        
    public void Show()
    {
        base.Open();
    }

    public void Hide()
    {
        base.Close();
    }

    public void SetCloseCallback(Action onStart)
    {
        this._closeBtn.SetCallback(onStart);
    }

    public void SetBuyCallback(Action onBuy)
    {
        this._buyBtn.SetCallback(onBuy);
    }

    public void SetItemName(string name)
    {
        this._message.text = $"{PopupBuyingMenu._frontMessage} {name}";
    }
}
