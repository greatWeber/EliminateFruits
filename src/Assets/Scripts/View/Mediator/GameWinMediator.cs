using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinMediator : EventMediator {

    [Inject]
    public GameWinView GameWin { get; set; }

    [Inject]
    public GameModel gameModel { get; set; }

    public override void OnRegister()
    {
        GameWin.btn_menu.onClick.AddListener(onMenuClick);
        GameWin.btn_retry.onClick.AddListener(onRetryClick);
        GameWin.btn_next.onClick.AddListener(onNextClick);
        dispatcher.AddListener(GameEvents.ViewEvent.GAME_WIN,onGameWin);

    }

    public override void OnRemove()
    {
       // GameWin.btn_menu.onClick.RemoveListener(onMenuClick);
       // GameWin.btn_retry.onClick.RemoveListener(onRetryClick);
        //GameWin.btn_next.onClick.RemoveListener(onNextClick);
        dispatcher.RemoveListener(GameEvents.ViewEvent.GAME_WIN, onGameWin);
    }

    private void OnDestroy()
    {
        OnRemove();
    }

    void onGameWin(IEvent en) {
        GameOverParams e = en.data as GameOverParams;
        if (GameWin == null) {
            dispatcher.RemoveListener(GameEvents.ViewEvent.GAME_WIN, onGameWin);
            return;
        }
        gameModel.currentScore = e.score;
        //保存分数
        gameModel.SetScore(gameModel.CurrentLevel, gameModel.currentScore);
        GameWin.Show(e.target,e.score);
        
    }

    void onMenuClick() {
        SceneManager.LoadScene(2,LoadSceneMode.Single);
    }

    void onRetryClick() {
        dispatcher.Dispatch(GameEvents.CommendEvent.START_GAME,-1);
    }

    void onNextClick() {
        dispatcher.Dispatch(GameEvents.CommendEvent.START_GAME, -2);

    }
}
