using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchView : View {

    /// <summary>
    /// view视图是不可以直接发请求到command的，一般要通过Mediator转发
    /// 但也可以通过以下方式发送请求
    /// </summary>
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    private GameObject PrevFruit;
    private int PrevID;

    private bool needMusic;

    private List<GameObject> selectedList = new List<GameObject>();

    private List<GameObject> Lines = new List<GameObject>();

    public bool isStop = false;
	
	// Update is called once per frame
	void Update () {
        if (isStop)
            return;
        //把屏幕坐标转化成世界坐标
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //鼠标按下
        if (Input.GetMouseButtonDown(0)) {
            Collider2D col = Physics2D.OverlapPoint(point);
            //当检测到的是水果时，显示并保存上一个水果
            if (col != null && col.name.Contains("fruit"))
            {
                GameObject tempFruit = col.gameObject;
                FruitView fruitView = tempFruit.GetComponent<FruitView>();
                fruitView.Selected = true;
                PrevID = fruitView.fruitID;
                PrevFruit = tempFruit;
                selectedList.Add(tempFruit);

                needMusic = true;
            }
            else {
                return;
            }

        }

        //鼠标一直按下
        if (Input.GetMouseButton(0)) {
            Collider2D col = Physics2D.OverlapPoint(point);
            if (col != null && col.name.Contains("fruit"))
            {
                if (needMusic) {
                    SoundManager.Instance.onPlayClip("selected");
                    needMusic = false;
                }
                GameObject tempFruit = col.gameObject;
                FruitView fruitView = tempFruit.GetComponent<FruitView>();
                //判断是否拖拽回原来的水果上
                if (tempFruit != PrevFruit) {
                    int tempID = fruitView.fruitID;

                    //判断是否为同一种水果
                    if (tempID == PrevID) {

                        //判断距离
                        if (CheckDistance(tempFruit, PrevFruit)) {

                            //判断是否为以前选择过的水果，当是水果数组倒数第二个的时候，取消选择倒数第一个水果
                            if (selectedList.Contains(tempFruit))
                            {
                                //判断是否为倒数第二个
                                if (selectedList.Count > 1 && selectedList[selectedList.Count - 2] == tempFruit)
                                {
                                    //
                                    PoolManager.Instance.HideObject(Lines[Lines.Count-1]);
                                    Lines.RemoveAt(Lines.Count-1);
                                    //取消选择
                                    GameObject fruit = selectedList[selectedList.Count - 1];
                                    fruit.GetComponent<FruitView>().Selected = false;
                                    selectedList.Remove(fruit);
                                    PrevFruit = tempFruit;
                                    needMusic = false;
                                }
                            }
                            else
                            {
                                //
                                CreateLine(tempFruit, PrevFruit);
                                //拖拽到新水果
                                fruitView.Selected = true;
                                PrevFruit = tempFruit;
                                PrevID = fruitView.fruitID;
                                selectedList.Add(tempFruit);
                                needMusic = true;

                               
                            }
                        }
                        

                    }

                }
            }
            else
            {
                return;
            }
        }

        //鼠标抬起
        if (Input.GetMouseButtonUp(0)) {
            if (selectedList.Count > 1)
            {
                //消除水果
                dispatcher.Dispatch(GameEvents.CommendEvent.DESTROY_FRUIT,selectedList);
            }
            else if(selectedList.Count == 1) {
                GameObject fruit = selectedList[0];
                fruit.GetComponent<FruitView>().Selected = false;
            }
            selectedList.Clear();

            PoolManager.Instance.HideAllObject("Line");
            Lines.Clear();
        }
	}

    bool CheckDistance(GameObject tempFruit, GameObject prevFruit) {
        Vector2 tempPos = tempFruit.GetComponent<FruitView>().MapPosition;
        Vector2 prevPos = prevFruit.GetComponent<FruitView>().MapPosition;

        return Mathf.Abs(tempPos.x - prevPos.x) <= 1 && Mathf.Abs(tempPos.y - prevPos.y) <= 1;
    }

    void CreateLine(GameObject tempFruit, GameObject prevFruit) {
        Vector2 linePos = new Vector2(
            (tempFruit.transform.position.x + prevFruit.transform.position.x)/2,
            (tempFruit.transform.position.y + prevFruit.transform.position.y) / 2
            );
        Quaternion rotation = Quaternion.Euler(0,0, GetRotationZ(tempFruit,prevFruit));
        GameObject go = PoolManager.Instance.GetObject("Line");
        go.transform.position = linePos;
        go.transform.rotation = rotation;
        Lines.Add(go);
    }

    float GetRotationZ(GameObject tempFruit, GameObject prevFruit) {
        Vector2 tempPos = tempFruit.GetComponent<FruitView>().MapPosition;
        Vector2 prevPos = prevFruit.GetComponent<FruitView>().MapPosition;

        if (tempPos.x == prevPos.x) {
            return 0f;
        }
        if (tempPos.y == prevPos.y) {
            return 90f;
        }
        float temp = (tempPos.x - prevPos.x) * (tempPos.y - prevPos.y);
        if (temp < 0)
        {
            return -45f;
        }
        else {
            return 45f;
        }
    }
}
