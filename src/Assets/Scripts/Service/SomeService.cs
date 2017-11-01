using System;
using UnityEngine;


/// <summary>
/// 服务器接口
/// 一个url地址
/// 一个监听回调
/// </summary>
public interface SomeService  {

    string URL { get; set; }
    void AddListener(Action<string> listener);
	
}
