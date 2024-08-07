using Cysharp.Threading.Tasks;
using UnityEngine;

public class Helper
{
    public static float GetNumberWithComma(float number)
    {
        float round = Mathf.Round(number * 100.0f) * 0.01f; // 2 decimal
        return round;
    }

    public static float getNumberWithPercent(float valid , float total)
    {
        float percent = (valid * total) / 100;
        return percent;
    }

    public static float getRandomRang(float min , float max)
    {
        float randomRang = Random.Range(min, max);
        return randomRang;
    }

    public static async UniTask DelayAsync(float delay)
    {
        await UniTask.Delay((int)(delay * 1000));
    }
}