using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestMapInfoCommand : Command {

    [Inject]
    public SomeService GameService { get; set; }

    [Inject]
    public GameModel GameModel { get; set; }

    public override void Execute()
    {
        GameService.URL = "mapInfo";
        GameService.AddListener(onComplete);
    }

    private void onComplete(string json) {
        MapInfoList mapInfoList = JsonUtility.FromJson<MapInfoList>(json);

        GameModel.MapInfoList = mapInfoList.MapList;
    }
}
