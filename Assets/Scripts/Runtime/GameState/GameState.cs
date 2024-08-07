public abstract class GameState
{
    protected GameManager manager;
    protected GameController game;
    protected UIController ui;
    protected AdsInitializer ads;
    protected DataManager data;
    protected EventEmitter emitter;

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void SubscribeEvent();
    public abstract void UnSubscribeEvent();
    protected GameState(GameManager manager)
    {
        this.manager = manager;
        this.game = manager.gameController;
        this.ui = manager.uiController;
        this.ads = manager.adsInitializer;
        this.data = DataManager.Instance;
        this.emitter = EventEmitter.Instance;
    }

    protected void Init()
    {
        game.Init();
    }

    //////////////////////////////////////////////

    protected void SaveSettingData()
    {
        data.SaveSettings();
    }

    protected void UpdateSettingData()
    {
        data.LoadSettings();
        UpdateScore();
        UpdateBalance();
    }

    protected void ClearSettingData()
    {
        data.ClearGameSettings();
    }

    //////////////////////////////////////////////

    protected void UpdateScore()
    {
        int score = data.scoreData.Score;
        ui.game.SetScore(score);
    }

    protected void ResetScore()
    {
        data.scoreData.Score = 0;
        int score = data.scoreData.Score;
        ui.game.SetScore(score);
    }

    protected void UpdateBalance()
    {
        int coin = data.coinData.Coin;
        ui.game.SetCoin(coin);
    }

    //////////////////////////////////////////////

    protected void TouchScreenInteractive(bool isInteractive)
    {
        ui.touchScreenArea.SetInteractive(isInteractive);
    }
}