using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool {

    public string Name;

    [SerializeField]
    private int MaxCount;

    [SerializeField]
    private GameObject prefab; //池子的对象

    [System.NonSerialized]
    private List<GameObject> PoolList = new List<GameObject>();


    public bool Contains(GameObject go) {
        return PoolList.Contains(go);
    }

    public GameObject GetObject() {
        GameObject go = null;
        for (int i=0;i<PoolList.Count;i++) {
            //是否显示
            
            if (!PoolList[i].activeSelf) {
                go = PoolList[i];
                go.SetActive(true);
                break;
            }
        }

        //当池子里面没有对应的对象的时候，创建一个
        if (go == null) {
            //多出来的对象销毁
            if (PoolList.Count >= MaxCount) {
                GameObject.Destroy(PoolList[0]);
                PoolList.RemoveAt(0);

            }
            go = GameObject.Instantiate<GameObject>(prefab);
            PoolList.Add(go);
        }
        //发送信息改变状态
        go.SendMessage("BeforeGetObject",SendMessageOptions.DontRequireReceiver);

        return go;
    }

    public void HideObject(GameObject go) {
        if (PoolList.Contains(go)) {
            go.SendMessage("BeforeHideObject", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    public void HideAllObject() {
        for (int i=0;i<PoolList.Count;i++) {
            if (PoolList[i].activeSelf) {
                HideObject(PoolList[i]);
            }
        }
    }

    public void InitPool() {
        PoolList = new List<GameObject>();
    }
}
