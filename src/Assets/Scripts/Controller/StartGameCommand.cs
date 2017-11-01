using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameCommand : EventCommand {


    [Inject]
    public GameModel GameModel { get; set; }

    public override void Execute()
    {
        int level = (int)this.evt.data;
        if (level == -1)
        {
            //重玩
            GameModel.CurrentLevel = GameModel.CurrentLevel;
        }
        else if (level == -2)
        {
            //下一关
            if (GameModel.CurrentLevel < GameModel.MapInfoList.Count) {
                GameModel.CurrentLevel++;
            }
            
        }
        else {
            GameModel.CurrentLevel = level;
        }
        SceneManager.LoadScene(3,LoadSceneMode.Single);
        PoolManager.Instance.InitAllPool();
    }
}
