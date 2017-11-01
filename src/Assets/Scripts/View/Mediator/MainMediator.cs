using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMediator : EventMediator {

    [Inject]
    public MainView mainView { get; set; }

    [Inject]
    public GameModel gameModel { get; set; }

    public override void OnRegister()
    {
        int target = gameModel.currentTarget;
        int score = gameModel.currentScore;
        int level = gameModel.CurrentLevel;
        float time = gameModel.currentTime;

        mainView.InitInfo(target,score,level);
        StartCoroutine(mainView.CountDown(time));

        mainView.dispatcher.AddListener(GameEvents.ViewEvent.Time_UP,onTimeUP);
        dispatcher.AddListener(GameEvents.ViewEvent.CHANGE_SCORE,onChangeScore);
    }

    public override void OnRemove()
    {
        mainView.dispatcher.RemoveListener(GameEvents.ViewEvent.Time_UP, onTimeUP);
        dispatcher.RemoveListener(GameEvents.ViewEvent.CHANGE_SCORE, onChangeScore);
    }

    private void OnDestroy()
    {
        OnRemove();
    }

    public void onTimeUP() {
        dispatcher.Dispatch(GameEvents.CommendEvent.GAME_OVER);
    }

    void onChangeScore(IEvent en) {
        int score = (int)en.data;
        mainView.onChangeScore(score);
    }
}
