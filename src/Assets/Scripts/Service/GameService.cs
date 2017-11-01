using strange.extensions.context.api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 获取游戏数据
/// </summary>
public class GameService : SomeService{
    public string URL
    {
        get;
        set;
    }
    /// <summary>
    /// 加上Inject标签  这样就可以进行绑定了
    /// Inject标签实现接口，而不是实例化类
    /// 在绑定多个的时候就需要利用  名称映射 来进行区分
    /// </summary>
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject ContextView { get; set; }


    public void AddListener(Action<string> listener)
    {
        TextAsset ta = Resources.Load<TextAsset>(URL);
        if (listener != null)
        {
            //回调函数
            listener(ta.text);
        }
        //ContextView.GetComponent<Root>().StartCoroutine(GetData(listener));
    }

    private IEnumerator GetData(Action<string> listener) {
        WWW www = new WWW(URL);
        while (!www.isDone) {
            yield return www;
        }

        //判断是否报错
        if (String.IsNullOrEmpty(www.error)) {
            if (listener != null) {
                //回调函数
                listener(www.text);
            }
        }
    }
}
