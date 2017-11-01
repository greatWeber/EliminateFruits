using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoseMediator : EventMediator {

    [Inject]
    public GameLoseView GameLose { get; set; }

    public override void OnRegister()
    {
        GameLose.btn_menu.onClick.AddListener(onMenuClick);
        GameLose.btn_restart.onClick.AddListener(onRestartClick);
        dispatcher.AddListener(GameEvents.ViewEvent.GAME_LOSE,onGameLose);
        //dispatcher.AddListener(GameEvents.CommendEvent.REMOVE_LOSE, OnRemove);
    }

    public override void OnRemove()
    {
        //GameLose.btn_menu.onClick.RemoveListener(onMenuClick);
        //GameLose.btn_restart.onClick.RemoveListener(onRestartClick);
        dispatcher.RemoveListener(GameEvents.ViewEvent.GAME_LOSE, onGameLose);
        //dispatcher.RemoveListener(GameEvents.CommendEvent.REMOVE_LOSE, OnRemove);
    }

    private void OnDestroy()
    {
        //dispatcher.Dispatch(GameEvents.CommendEvent.REMOVE_WIN);
        OnRemove();
    }

    private void onGameLose(IEvent evt) {
        GameOverParams e = evt.data as GameOverParams;
        if (GameLose == null) {
            dispatcher.RemoveListener(GameEvents.ViewEvent.GAME_LOSE, onGameLose);
            return;
        }
        GameLose.Show(e.target,e.score);
       
    }

    void onMenuClick() {
        SceneManager.LoadScene(2,LoadSceneMode.Single);
    }

    void onRestartClick() {
        dispatcher.Dispatch(GameEvents.CommendEvent.START_GAME,-1);
    }
}
