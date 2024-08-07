using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public PopupSelection select { get => _popupSelect; }
    [SerializeField] private PopupSelection _popupSelect;
    public PopupPause pause { get => _popupPause; }
    [SerializeField] private PopupPause _popupPause;
    public PopupReward reward { get => _popupReward; }
    [SerializeField] private PopupReward _popupReward;

    public PopupPowerUp powerUp { get => _popupPowerUp; }
    [SerializeField] private PopupPowerUp _popupPowerUp;

    private List<string> _list = new List<string>();

    public bool isShow
    {
        get { return _list.Count > 0; }
    }

    ////////////////////////////////////////////////

    public void ShowSelectPopup()
    {
        setIDCallback(_popupSelect);
        _popupSelect.Show();
    }

    ////////////////////////////////////////////////

    public void ShowPausePopup()
    {
        setIDCallback(_popupPause);
        _popupPause.Show();
    }

    ////////////////////////////////////////////////

    public void ShowRewardPopup()
    {
        setIDCallback(_popupReward);
        _popupReward.Show();
    }

    public void SetRewardPriceValue(float menu, float addon, float premium, float total)
    {
        _popupReward.SetMenuPrice(menu.ToString());
        _popupReward.SetAddonPrice(addon.ToString());
        _popupReward.SetPremiumAddonPrice(premium.ToString());
        _popupReward.SetTotalPrice(total.ToString());
    }

    ////////////////////////////////////////////////

    public void ShowPowerUpPopup(float duration)
    {
        setIDCallback(_popupPowerUp);
        _popupPowerUp.Show();
        _popupPowerUp.UpdateTimerBar(duration);
    }

    ////////////////////////////////////////////////

    private void setIDCallback(PopupModule popup)
    {
        popup.OnOpen(() => {
            AddPopup(popup.gameObject.name);
            Debug.Log("List popup : " + String.Join(" | ", _list));
        });
        popup.OnClose(() => {
            RemovePopup(popup.gameObject.name);
            Debug.Log("List popup : " + String.Join(" | ", _list));
        });
    }

    private void AddPopup(string popupName)
    {
        if (_list.Contains(popupName) == false)
        {
            _list.Add(popupName);
        }
        else
        {
            Debug.LogError("Cannot open the same popup : " + popupName);
        }
    }

    private void RemovePopup(string popupName)
    {
        if (_list.Contains(popupName))
        {
            int index = _list.FindIndex(x => x == popupName);
            _list.RemoveRange(index, 1);
        }
        else
        {
            Debug.LogError("Cannot find popup : " + popupName);
        }
    }
}
