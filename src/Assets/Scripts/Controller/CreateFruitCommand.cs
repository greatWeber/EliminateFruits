using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFruitCommand : EventCommand
{
    [Inject]
    public GameModel gameModel { get; set; }

    public override void Execute()
    {
        CreateFruitParams e = this.evt.data as CreateFruitParams;

        GameObject fruit = PoolManager.Instance.GetObject("Fruit");
        fruit.transform.parent = e.parent;
        fruit.transform.position = e.WorldPosition;
        //
        gameModel.Fruits.Add(fruit);

        //update fruitUI
        int fruitId = gameModel.currentFruit[Random.Range(0, 5)];
        fruit.GetComponent<FruitView>().InitFruit(fruitId,e.MapPosition);
    }

}
