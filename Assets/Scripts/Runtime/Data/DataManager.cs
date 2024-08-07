using UnityEngine;

public class DataManager
{
    private static DataManager _instance = null;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }
            return _instance;
        }
    }

    public readonly ScoreData scoreData = new ScoreData();
    public readonly CoinData coinData = new CoinData();
    public readonly PowerUpData powerUpData = new PowerUpData();
    public readonly MenuResultData menuResultData = new MenuResultData();

    //////////////////////////////////////////////

    public void LoadSettings()
    {
        scoreData.Score = PlayerPrefs.GetInt("score", scoreData.Score);
        coinData.Coin = PlayerPrefs.GetInt("coin", coinData.Coin);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("score", scoreData.Score);
        PlayerPrefs.SetInt("coin", coinData.Coin);
        PlayerPrefs.Save();
    }

    public void ClearGameSettings()
    {
        PlayerPrefs.DeleteAll();
    }

    //////////////////////////////////////////////
}