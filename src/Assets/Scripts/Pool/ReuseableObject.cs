using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象是否重复可用的抽象类
/// </summary>
public abstract class ReuseableObject : MonoBehaviour {

    /// <summary>
    /// 取出对象之前的重置
    /// </summary>
    public abstract void BeforeGetObject();

    /// <summary>
    /// 放回对象之前的还原
    /// </summary>
    public abstract void BeforeHideObject();
}
