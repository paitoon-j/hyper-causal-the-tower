using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpData
{
    private HeadStartData _headStartData = new HeadStartData();
    private SlowTimeData _slowTimeData = new SlowTimeData();

    public HeadStartData HeadStartData { get { return _headStartData; } }
    public SlowTimeData SlowTimeData { get { return _slowTimeData; } }
}
