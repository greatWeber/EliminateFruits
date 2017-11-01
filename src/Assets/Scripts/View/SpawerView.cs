using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawerView : EventView {

    private float[] posX = { -2.01f, -1.35f, -0.67f, 0, 0.67f, 1.35f, 2.01f, 2.68f };

    private float posY = 5f;

    public void InitFruit() {
        for (int i=0;i<7;i++) {
            for (int j=0;j<7; j++) {
                CreateFruitParams e = new CreateFruitParams
                {
                    MapPosition = new Vector2(i, j),
                    WorldPosition = new Vector2(posX[i], posY-j*0.3f),
                    parent = transform

                };
                dispatcher.Dispatch(GameEvents.ViewEvent.CREATE_FRUIT,e);
            }
        }
    }

    public void NewFruit(List<int> XList, List<int> YList) {
        int[] height = new int[7];
        for (int i=0;i< XList.Count;i++) {
            int x = XList[i];
            int y = YList[i];
            height[x]++;
            CreateFruitParams e = new CreateFruitParams
            {
                MapPosition = new Vector2(x, y),
                WorldPosition = new Vector2(posX[x], posY - height[x]),
                parent = transform

            };
            dispatcher.Dispatch(GameEvents.ViewEvent.CREATE_FRUIT, e);
        }
    }

   
}
