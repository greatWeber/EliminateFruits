using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawerMediator : EventMediator
{
    [Inject]
    public SpawerView spawerView { get; set; }

    public override void OnRegister()
    {
        dispatcher.AddListener(GameEvents.ViewEvent.NEW_FRUIT,onNewFruit);
        spawerView.dispatcher.AddListener(GameEvents.ViewEvent.CREATE_FRUIT,onCreateFruit);
        spawerView.InitFruit();
    }

    public override void OnRemove()
    {
        spawerView.dispatcher.RemoveListener(GameEvents.ViewEvent.CREATE_FRUIT, onCreateFruit);
        dispatcher.RemoveListener(GameEvents.ViewEvent.NEW_FRUIT, onNewFruit);
    }

    private void OnDestroy()
    {
        OnRemove();
    }

    void onCreateFruit(IEvent en) {
        CreateFruitParams e = en.data as CreateFruitParams;
        dispatcher.Dispatch(GameEvents.CommendEvent.CREATE_FRUIT,e);
    }

    void onNewFruit(IEvent en) {
        NewFruitParams e = en.data as NewFruitParams;
        spawerView.NewFruit(e.XList,e.YList);
    }
}
