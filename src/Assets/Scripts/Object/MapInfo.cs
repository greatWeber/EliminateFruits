using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class MapInfo {
    public int TargetSource;
    public float LimitTime;
    public int[] FruitIndex = new int[5];
}

[Serializable]
public class MapInfoList {
    public List<MapInfo> MapList;
}
