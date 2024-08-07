using UnityEngine;

public class UIController : MonoBehaviour
{
    public HomeUI home { get => _homeUI; }
    [SerializeField] private HomeUI _homeUI;

    public GameUI game { get => _gameUI; }
    [SerializeField] private GameUI _gameUI;

    public PopupController popup { get => _popupUI; }
    [SerializeField] private PopupController _popupUI;

    public TouchScreen touchScreenArea { get => _touchScreenArea; }
    [SerializeField] private TouchScreen _touchScreenArea;

    public void SetHomeUI()
    {
        _touchScreenArea.SetActive(true);
        _gameUI.SetActive(false);
        _homeUI.SetActive(true);
    }

    public void SetMainGameUI()
    {
        _touchScreenArea.SetActive(true);
        _gameUI.SetActive(true);
        _homeUI.SetActive(false);
    }

    public void SetActive(bool isActive)
    {
        _gameUI.SetActive(isActive);
        _touchScreenArea.SetActive(isActive);
    }

    public void SetInteractive(bool isInteractive)
    {
        _gameUI.SetInteractive(isInteractive);
        _touchScreenArea.SetInteractive(isInteractive);
    }
}