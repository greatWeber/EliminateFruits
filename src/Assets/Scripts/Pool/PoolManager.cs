using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 管理所有对象池
/// </summary>
public class PoolManager  {

    private static PoolManager instance;

    public static PoolManager Instance {
        get {
            if (instance == null) {
                instance = new PoolManager();
            }
            return instance;
        }
    }

    //保存单个对象池的字典
    public Dictionary<string, ObjectPool> poolDict = new Dictionary<string, ObjectPool>();

    public const string configPath = "Assets/Scripts/Pool/Resources/pool.asset";

    public PoolManager() {
        ObjectPoolList objectPoolList = Resources.Load<ObjectPoolList>("pool");
        foreach (ObjectPool pool in objectPoolList.poolList) {
            this.poolDict.Add(pool.Name,pool);
        }
    }

    //根据对象池名字获取对象
    public GameObject GetObject(string poolName) {
        if (poolDict.ContainsKey(poolName))
        {
            ObjectPool pool = poolDict[poolName];
            return pool.GetObject();
        }
        else {
            Debug.LogError("没有"+poolName+"对象池");
            return null;
        }
    }

    public void HideObject(GameObject go) {
        foreach (ObjectPool pool in poolDict.Values) {
            if (pool.Contains(go)) {
                pool.HideObject(go);
                return;
            }
        }
    }

    public void HideAllObject(string poolName) {
        if (poolDict.ContainsKey(poolName))
        {
            ObjectPool pool = poolDict[poolName];
            pool.HideAllObject();
        }
        else {
            Debug.LogError("没有" + poolName + "对象池");
            return;
        }
    }

    //初始化，防止再次运行游戏时报空指针的错误
    public void InitAllPool() {
        foreach (ObjectPool pool in poolDict.Values)
        {
            pool.InitPool();
        }
    }
}
