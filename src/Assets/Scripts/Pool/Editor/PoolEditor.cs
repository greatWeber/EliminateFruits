using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 生成一个可配置的文件
/// </summary>
public class PoolEditor : MonoBehaviour {

    //在工具栏中显示
    [MenuItem("createPool/create poolConfig")]
    static void CreatePoolList() {
        ObjectPoolList poolList = ScriptableObject.CreateInstance<ObjectPoolList>();
        string path = PoolManager.configPath;
        AssetDatabase.CreateAsset(poolList, path);
        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("提示","创建成功","OK");

    }
}
