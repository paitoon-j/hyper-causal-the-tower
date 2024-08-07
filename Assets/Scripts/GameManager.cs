using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIController uiController { get => _uiController; }
    [SerializeField] private UIController _uiController;
    public GameController gameController { get => _gameController; }
    [SerializeField] private GameController _gameController;
    public AdsInitializer adsInitializer { get => _adsInitializer; }
    [SerializeField] private AdsInitializer _adsInitializer;

    private GameState _currentState;

    void Start()
    {
        ChangeState(EState.LOGIN);
    }

    public void ChangeState(EState state)
    {
        if (_currentState != null)
        {
            _currentState.OnExit();
            _currentState.UnSubscribeEvent();
        }
        _currentState = GetGameState(state);
        _currentState.OnEnter();
        _currentState.SubscribeEvent();
    }

    private GameState GetGameState(EState state)
    {
        switch (state) 
        {
            case EState.LOGIN: return new GameLoginState(this);
            case EState.HOME: return new GameHomeState(this);
            case EState.MAIN: return new GameMainState(this);
            default: return null;
        }
    }
}
