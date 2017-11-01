using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFruitCommand : EventCommand {

    [Inject]
    public GameModel gameModel { get; set; }

   

    public override void Execute()
    {
        List<GameObject> selectedList = this.evt.data as List<GameObject>;

        NewFruitParams e = new NewFruitParams();

       

        for (int i = 0; i < selectedList.Count; i++) {
            //判断是否有额外加分
            if (selectedList[i].GetComponent<FruitView>().Power != 0)
            {
                int fruitPower = selectedList[i].GetComponent<FruitView>().Power;
                Vector2 map = selectedList[i].GetComponent<FruitView>().MapPosition;
                if (fruitPower == 1)
                {
                    powerEffect1(map, selectedList);
                    SoundManager.Instance.onPlayClip("props1music");
                }
                else if (fruitPower == 2)
                {
                    powerEffect2(map, selectedList);
                    SoundManager.Instance.onPlayClip("props2music");
                }
                else if (fruitPower == 3)
                {
                    powerEffect3(map, selectedList);
                    SoundManager.Instance.onPlayClip("props3music");
                }
            }
        }
        int score = GetScore(selectedList.Count);
        //加分
        for (int i=0;i<selectedList.Count;i++) {
           
            //创建分数的预设
            GameObject ScoreGo = PoolManager.Instance.GetObject("Score");
            ScoreGo.GetComponent<ScoreController>().InitScore(score);
            ScoreGo.transform.position = selectedList[i].transform.position;

            gameModel.currentScore += score;
            Vector2 pos = selectedList[i].GetComponent<FruitView>().MapPosition;
            //隐藏水果并且重置数组
            ResetMap(pos, selectedList[i]);

            //创建爆炸特效
            GameObject explodeGo = PoolManager.Instance.GetObject("ExplodeEffect");
            explodeGo.transform.position = selectedList[i].transform.position;

            //创建新的水果
            int y = 6;
            foreach (GameObject item in gameModel.Fruits) {
                if (item.GetComponent<FruitView>().MapPosition.x == pos.x) {
                    y--;
                }
            }
            e.XList.Add((int)pos.x);
            e.YList.Add(y);
        }

        //添加新的水果
        dispatcher.Dispatch(GameEvents.ViewEvent.NEW_FRUIT,e);

        //
        int power = GetBound(selectedList.Count);
        while (true) {
            int index = Random.Range(0,gameModel.Fruits.Count);
            if (gameModel.Fruits[index].GetComponent<FruitView>().Power == 0) {
                gameModel.Fruits[index].GetComponent<FruitView>().Power = power;
                break;
            }
        }

        //更新UI分数
        dispatcher.Dispatch(GameEvents.ViewEvent.CHANGE_SCORE, gameModel.currentScore);
    }

    void ResetMap(Vector2 pos, GameObject fruit) {
        gameModel.Fruits.Remove(fruit);
        for (int i=0;i<gameModel.Fruits.Count;i++) {

            Vector2 mapPos = gameModel.Fruits[i].GetComponent<FruitView>().MapPosition;

            if (mapPos.x == pos.x && mapPos.y <= pos.y) {
                gameModel.Fruits[i].GetComponent<FruitView>().MapPosition.y += 1;
            }
        }

        PoolManager.Instance.HideObject(fruit);
    }

    int GetScore(int counts) {
        int score = 0;
        if (counts < 3) {
            score = 5;
        } else if (counts >= 3 && counts < 6) {
            score = 10;
        } else if (counts >= 6 && counts < 12) {
            score = 10;
            //创建分数的预设
            GameObject ScoreGo = PoolManager.Instance.GetObject("Score");
            ScoreGo.GetComponent<ScoreController>().InitScore(100);
            ScoreGo.transform.position = new Vector2();
            ScoreGo.GetComponent<ScoreController>().Special = true;

            gameModel.currentScore += 100;

            SoundManager.Instance.onPlayClip("goodmusic");

        } else if (counts >= 12) {
            score = 20;
            //创建分数的预设
            GameObject ScoreGo = PoolManager.Instance.GetObject("Score");
            ScoreGo.GetComponent<ScoreController>().InitScore(200);
            ScoreGo.transform.position = new Vector2();
            ScoreGo.GetComponent<ScoreController>().Special = true;

            gameModel.currentScore += 200;
            SoundManager.Instance.onPlayClip("goodmusic");
        }
        return score;
    }

    int GetBound(int count) {
        int power = 0;
        if (count > 6 && count <= 8) {
            power = 1;
        } else if (count > 8 && count <= 10) {
            power = 2;
        } else if (count >10) {
            power = 3;
        }
        return power;
    }

    /// <summary>
    /// 消除周围的水果
    /// </summary>
    void powerEffect1(Vector2 center, List<GameObject>fruits) {
        foreach (GameObject fruit in gameModel.Fruits) {
            if (fruits.Contains(fruit) || fruit.GetComponent<FruitView>().Power != 0) {
                continue;
            }
            Vector2 map = fruit.GetComponent<FruitView>().MapPosition;
            if (Mathf.Abs(center.x-map.x)<=1 && Mathf.Abs(center.y-map.y)<=1) {
                fruits.Add(fruit);
            }
        }
    }

    /// <summary>
    /// 消除一行或者一列的水果
    /// </summary>
    void powerEffect2(Vector2 center, List<GameObject>fruits) {
        int r = Random.Range(0,2);
        if (r == 0)
        {
            //消除一行
            foreach (GameObject fruit in gameModel.Fruits)
            {
                if (fruits.Contains(fruit) || fruit.GetComponent<FruitView>().Power != 0)
                {
                    continue;
                }
                Vector2 map = fruit.GetComponent<FruitView>().MapPosition;
                if (map.x == center.x)
                {
                    fruits.Add(fruit);
                }
            }
        }
        else {
            foreach (GameObject fruit in gameModel.Fruits)
            {
                if (fruits.Contains(fruit) || fruit.GetComponent<FruitView>().Power != 0)
                {
                    continue;
                }
                Vector2 map = fruit.GetComponent<FruitView>().MapPosition;
                if (map.y == center.y)
                {
                    fruits.Add(fruit);
                }
            }
        }
    }

    /// <summary>
    /// 消除一行和一列的水果
    /// </summary>
    void powerEffect3(Vector2 center, List<GameObject> fruits)
    {
        foreach (GameObject fruit in gameModel.Fruits)
        {
            if (fruits.Contains(fruit) || fruit.GetComponent<FruitView>().Power != 0)
            {
                continue;
            }
            Vector2 map = fruit.GetComponent<FruitView>().MapPosition;
            if (map.y == center.y || map.x == center.x)
            {
                fruits.Add(fruit);
            }
        }
    }
}
