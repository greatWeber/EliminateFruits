using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : MonoBehaviour {

    private static ResManager instance;
    public static ResManager Instance {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    //字典：保存已加载的预设
    public Dictionary<string, PrefabToLoad> LoadedPrefabDic = new Dictionary<string, PrefabToLoad>();

    /// <summary>
    /// 根据路径加载预设
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public UnityEngine.Object LoadPrefab(string path) {
        if (string.IsNullOrEmpty(path)) return null;

        if (LoadedPrefabDic.ContainsKey(path))
        {
            return LoadedPrefabDic[path].Prefab;
        }
        else {
            PrefabToLoad prefab = new PrefabToLoad() {

                prefabName = path,

                Prefab = Resources.Load(path)

            };
            LoadedPrefabDic.Add(path,prefab);

            return LoadedPrefabDic[path].Prefab;
        }
    }

    /// <summary>
    /// 根据路径卸载预设
    /// 加载的东西要放在Resources文件夹下面
    /// </summary>
    /// <param name="path"></param>
    public void unLoadPrefab(string path) {

        if (!LoadedPrefabDic.ContainsKey(path)) return;

        Resources.UnloadAsset(LoadedPrefabDic[path].Prefab);

        LoadedPrefabDic[path] = null;

        LoadedPrefabDic.Remove(path);
    }

    /// <summary>
    /// 卸载所有预设
    /// </summary>
    public void unLoadAll() {

        foreach (KeyValuePair<string,PrefabToLoad> k in LoadedPrefabDic) {

            LoadedPrefabDic[k.Key] = null;
        }
        LoadedPrefabDic.Clear();

        Resources.UnloadUnusedAssets();
    }

   
}

public class PrefabToLoad
{
    public string prefabName;

    public UnityEngine.Object Prefab;
}