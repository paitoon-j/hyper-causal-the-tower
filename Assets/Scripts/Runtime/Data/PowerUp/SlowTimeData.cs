
public class SlowTimeData
{
    private float _speedDefault = 1;
    private float _moveSlowdownPercent = 50f;
    private float _durationSlowdownSecond = 10f;
    private bool _isSlowTime = false;

    public float SlowTime
    {
        get { return Helper.getNumberWithPercent(this._speedDefault, this._moveSlowdownPercent); }
        set { this._speedDefault = value; }
    }

    public float Duration
    {
        get { return this._durationSlowdownSecond; }
    }

    public bool IsSlowTime
    {
        get { return this._isSlowTime; }
        set { this._isSlowTime = value; }
    }
}
