using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCommand : EventCommand {

    [Inject]
    public GameModel gameModel { get; set; }

    public override void Execute()
    {
        int diff = gameModel.currentTarget - gameModel.currentScore;
        GameOverParams e = new GameOverParams
        {
            target = gameModel.currentTarget,
            score = gameModel.currentScore
        };
        if (diff > 0)
        {
            //输了
            dispatcher.Dispatch(GameEvents.ViewEvent.GAME_LOSE, e);
        }
        else {
            //win
            if(gameModel.CurrentLevel == gameModel.ReachedLevel)
            gameModel.ReachedLevel++;
            dispatcher.Dispatch(GameEvents.ViewEvent.GAME_WIN, e);
        }
    }
}
