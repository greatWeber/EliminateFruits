using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents {

    //命令事件
    public enum CommendEvent {
        REQUEST_MAPINFO = 0,
        UPDATE_MAPSHOW = 1,
        START_GAME = 2,
        GAME_OVER = 3,
        REMOVE_WIN = 4,
        REMOVE_LOSE = 5,
        CREATE_FRUIT = 6,
        DESTROY_FRUIT = 7
    }

    //视图事件
    public enum ViewEvent {
        UPDATE_MAPSHOW = 0,
        Time_UP = 1,
        GAME_LOSE = 2,
        GAME_WIN = 3,
        CREATE_FRUIT = 5,
        CHANGE_SCORE = 6,
        NEW_FRUIT = 7
    }

}
