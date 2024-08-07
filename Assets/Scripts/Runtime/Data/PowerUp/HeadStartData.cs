using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadStartData
{
    private int _createTowerConfig = 10;
    private int _createTowerDefault = 1;
    private bool _isHeadStart = false;

    public int StartCreateTowerConfig
    {
        get { return _createTowerConfig; }
        set { _createTowerConfig = value; }
    }

    public int StartCreateTowerDefault
    {
        get { return _createTowerDefault; }
        set { _createTowerDefault = value; }
    }


    public bool IsHeadStart
    {
        get { return _isHeadStart; }
        set { _isHeadStart = value; }
    }
}
