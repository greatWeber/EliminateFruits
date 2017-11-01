using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 第一个命令，一般用于初始化游戏
/// </summary>
public class FirstCommand : EventCommand {

    [Inject]
    public GameModel gameModel { get; set; }

    //命令执行的地方
    public override void Execute()
    {
        //放音乐
        SoundManager.Instance.onPlayBGM("music");


        //保存一个全局的游戏组件
        Object.DontDestroyOnLoad(GameObject.Find("Game"));

        //获取游戏数据
        //先在GameContext中绑定，然后在这里通过发送消息的方式执行方法
        dispatcher.Dispatch(GameEvents.CommendEvent.REQUEST_MAPINFO);

        //获取通关分数
        gameModel.InitScore();

        //加载下一个场景
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }
}
