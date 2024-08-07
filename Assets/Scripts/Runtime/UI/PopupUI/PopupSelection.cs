using UnityEngine;


public class PopupSelection : PopupModule
{
    [SerializeField] private ButtonModule _startBtn;
    [SerializeField] private ToggleModule _headStartToggle;
    [SerializeField] private ToggleModule _slowTimeToggle;

    public void Show()
    {
        base.Open();
        SetOnStartGameCallback();
        SetOnHeadStartCallback();
        SetOnSlowTimeCallback();
    }

    public void Hide()
    {
        base.Close();
    }

    private void SetOnStartGameCallback()
    {
        _startBtn.SetCallback(() =>
        {
            Debug.Log("Start game");
            EventEmitter.Instance.Emit(EGlobalEvent.START_GAME);
            Hide();
        });
    }

    private void SetOnHeadStartCallback()
    {
        _headStartToggle.SetToggleCallback((isOn) =>
        {
            Debug.Log($"headstart is {isOn}");
            EventEmitter.Instance.Emit(EGlobalEvent.ON_HEAD_START, isOn);
        });
    }

    private void SetOnSlowTimeCallback()
    {
        _slowTimeToggle.SetToggleCallback((isOn) =>
        {
            Debug.Log($"slowtime is {isOn}");
            EventEmitter.Instance.Emit(EGlobalEvent.ON_SLOW_TIME, isOn);
        });
    }
}
