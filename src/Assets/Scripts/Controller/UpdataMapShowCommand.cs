using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdataMapShowCommand : EventCommand {

    [Inject]
    public GameModel GameModel { get; set; }

    public override void Execute()
    {
        int ReachedLevel = GameModel.ReachedLevel;
        dispatcher.Dispatch(GameEvents.ViewEvent.UPDATE_MAPSHOW, ReachedLevel);
    }
}
