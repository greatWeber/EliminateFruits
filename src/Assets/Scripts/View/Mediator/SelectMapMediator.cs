using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMapMediator : EventMediator {

    [Inject]
    public SelectMapView selectMapView { get; set; }

    [Inject]
    public GameModel gameModel { get; set; }

    public override void OnRegister()
    {
        selectMapView.GetMapView();
        for (int i=0;i<selectMapView.mapViews.Length;i++) {
            selectMapView.mapViews[i].onClick += onMapClick;

        }
        selectMapView.btn_back.onClick.AddListener(onBackClick);

        //监听数据
        dispatcher.AddListener(GameEvents.ViewEvent.UPDATE_MAPSHOW,onUpdataShow);

        //发送请求
        dispatcher.Dispatch(GameEvents.CommendEvent.UPDATE_MAPSHOW);
    }

    public override void OnRemove()
    {
        selectMapView.btn_back.onClick.RemoveListener(onBackClick);
        dispatcher.RemoveListener(GameEvents.ViewEvent.UPDATE_MAPSHOW, onUpdataShow);
    }

    private void OnDestroy()
    {
        OnRemove();
    }

    private void onMapClick(int level) {
        //发送命令，开始游戏并传送level
        dispatcher.Dispatch(GameEvents.CommendEvent.START_GAME,level);
    }

    private void onBackClick() {
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }

    private void onUpdataShow(IEvent e) {
        int ReachedLevel = (int)e.data;
        for (int i=0;i< selectMapView.mapViews.Length; i++) {
            bool locked = selectMapView.mapViews[i].Level > ReachedLevel;
            selectMapView.mapViews[i].Locked = locked;
            if (selectMapView.mapViews[i].Level < ReachedLevel) {
                int level = selectMapView.mapViews[i].Level;
                int score = gameModel.GetScore(level);
                int target = gameModel.MapInfoList[level].TargetSource;
                selectMapView.mapViews[i].GetStar(score,target);
            }
        }
    }
}
