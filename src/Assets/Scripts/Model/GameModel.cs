using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel  {

    private List<MapInfo> mapInfoList;

    

   public List<MapInfo> MapInfoList {
        get { return mapInfoList; }
        set { mapInfoList = value; }
    }

    //每个水果的得分
    public int score = 10;

    private MapInfo currentMapInfo;

    public int currentTarget { get { return currentMapInfo.TargetSource; } }
    public float currentTime { get { return currentMapInfo.LimitTime; } }
    public int[] currentFruit { get { return currentMapInfo.FruitIndex; } }

    private int currentLevel;
    public int CurrentLevel {
        get { return currentLevel; }
        set {
            currentLevel = value;
            currentMapInfo = mapInfoList[value - 1];
            currentScore = 0;
            Fruits = new List<GameObject>();
        }
    }

    public int currentScore;

    //已经过关的最大关卡
    public int ReachedLevel {
        get {
            return PlayerPrefs.GetInt("ReachedLevel",1);
           //PlayerPrefs.SetInt("ReachedLevel", 1);
            //return 1;
        }
        set {
            value = Mathf.Clamp(value,1, mapInfoList.Count);
            PlayerPrefs.SetInt("ReachedLevel",value);
        }
    }

    //地图上的所有水果
    public List<GameObject> Fruits;

    //保存通关的分数
    private List<int> scoreList = new List<int>();

    public void InitScore() {
        int count = PlayerPrefs.GetInt("count",0);
        for (int i=0;i<count;i++) {
            scoreList.Add(PlayerPrefs.GetInt("level-"+(i+1),0));
        }
    }

    public void SetScore(int level,int score) {
        if (level > scoreList.Count)
        {
            scoreList.Add(score);
        }
        else {
            scoreList[level - 1] = score;
        }
        PlayerPrefs.SetInt("level-"+level,score);
        PlayerPrefs.SetInt("count", scoreList.Count);
    }

    public int GetScore(int level) {
        return level <= scoreList.Count ? scoreList[level - 1] : 0;
    }
}
