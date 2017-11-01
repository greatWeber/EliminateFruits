using strange.extensions.context.api;
using strange.extensions.context.impl;
using System;
using UnityEngine;

public class GameContext : MVCSContext {

    //重写这个方法
    public GameContext(MonoBehaviour view, bool autoMapping) : base(view, autoMapping)
	{

    }

    //绑定数据，命令
    //任何对象都要在这里绑定，然后才能注入或者调用
    protected override void mapBindings()
    {
        //Injection: 注入绑定.主要是用来绑定该类型对象到上下文，这样使得程序中各个地方可以通过contextview访问得到该对象。
        //这种绑定会生成对象。这种绑定是为了生成对象并且注入到指定对象中用的
        //ToSingleton: 单例映射
        injectionBinder.Bind<SomeService>().To<GameService>().ToSingleton();
        injectionBinder.Bind<GameModel>().To<GameModel>().ToSingleton();

        //command: 将命令绑定到方法中用

        commandBinder.Bind(GameEvents.CommendEvent.REQUEST_MAPINFO).To<RequestMapInfoCommand>();
        commandBinder.Bind(GameEvents.CommendEvent.UPDATE_MAPSHOW).To<UpdataMapShowCommand>();
        commandBinder.Bind(GameEvents.CommendEvent.START_GAME).To<StartGameCommand>();
        commandBinder.Bind(GameEvents.CommendEvent.GAME_OVER).To<GameOverCommand>();
        commandBinder.Bind(GameEvents.CommendEvent.CREATE_FRUIT).To<CreateFruitCommand>();
        commandBinder.Bind(GameEvents.CommendEvent.DESTROY_FRUIT).To<DestroyFruitCommand>();


        //绑定第一个命令
        commandBinder.Bind(ContextEvent.START).To<FirstCommand>().Once();

        //Mediator
        mediationBinder.Bind<WelcomeView>().To<WelcomeMediator>();
        mediationBinder.Bind<SelectMapView>().To<SelectMapMediator>();
        mediationBinder.Bind<MainView>().To<MainMediator>();
        mediationBinder.Bind<GameLoseView>().To<GameLoseMediator>();
        mediationBinder.Bind<GameWinView>().To<GameWinMediator>();
        mediationBinder.Bind<SpawerView>().To<SpawerMediator>();
    }

}
